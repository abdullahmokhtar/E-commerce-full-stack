using Backend.API.Dtos.Account;
using Backend.API.Helper;

namespace Backend.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserResetPasswordService _userResetPasswordService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, 
            ITokenService tokenService, 
            SignInManager<AppUser> signInManager,
            IUserResetPasswordService userResetPasswordService,
            IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userResetPasswordService = userResetPasswordService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Signup(SignUpDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is not null)
                return BadRequest($"Email {model.Email} is already signed up");

            user = await _userManager.FindByNameAsync(model.Name);

            if (user is not null)
                return BadRequest($"Name {model.Name} is taken");

            var appUser = new AppUser
            {
                Email = model.Email,
                UserName = model.Name.Trim().Replace(" ", ""),
                PhoneNumber = model.Phone
            };

            var result = await _userManager.CreateAsync(appUser, model.Password);

            if (!result.Succeeded)
                return BadRequest("Something went wrong please try again signup");


            return Ok(new UserDto
            {
                Email = appUser.Email,
                UserName = appUser.UserName,
                Token = _tokenService.GenerateToken(appUser)

            });
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(SignInDto model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
                return Unauthorized("Email or password is invalid");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if(!result.Succeeded)
                return Unauthorized("Email or password is invalid");

            return Ok(new UserDto 
            { Email = user.Email, 
                UserName = user.UserName,
                Token = _tokenService.GenerateToken(user) 
            });
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDto model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var code = await _userResetPasswordService.GenerateCode(model.Email);

            if (code == null)
                return BadRequest("Something went wrong");

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
                return NotFound("User Not signed up");

            //var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            //var resetLink = Url.Action("ResetPassword", "Account", new {token, email = user.Email}, Request.Scheme);

            var email = new Email
            {
                //Body = $"Please reset your password by clicking here: <a href='{resetLink}'>link</a>",
                Title = "Reset Password",
                Body = $"To reset your password.\n Submit this reset password code: {code}, Note this code is valid for 15 minutes only if you did notrequest a change of password, please ignore this email",
                To = model.Email
            };

            EmailSettigns.SendEmail(email);

            return Ok("Reset code sent to your email");
        }

        [HttpPost]
        public async Task<IActionResult> VerifyResetCode(ResetCodeDto model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _userResetPasswordService.VerifyCode(model.ResetCode))
                return Ok("Code Is Verified");
            else
                return BadRequest("Code not verified Please try again");
        }

        [HttpPut]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var isVerified = await _userResetPasswordService.IsVerified(model.Email);

            if (!isVerified)
                return BadRequest("Reset Code Is Not Verified");

            var appUser = await _userManager.FindByEmailAsync(model.Email);

            var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);


            var result = await _userManager.ResetPasswordAsync(appUser, token, model.Password);

            if (!result.Succeeded)
                return BadRequest("Something went wrong");
            await _userResetPasswordService.DeleteCode(appUser.Id);
            return Ok("Password reseted successfuly"); 
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDataDto>> GetLoggedUserData()
        {
            var userName = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(userName);
            return Ok(_mapper.Map<UserDataDto>(appUser));
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<UserDataDto>> UpdateLoggedUserData(UserDataDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userName = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(userName);
            appUser.PhoneNumber = model.PhoneNumber;
            appUser.UserName = model.UserName;
            appUser.Email = model.Email;
            var result =  await _userManager.UpdateAsync(appUser);
            if (!result.Succeeded) return BadRequest("Something went wrong");
            return Ok(_mapper.Map<UserDataDto>(appUser));
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateLoggedUserPassword(ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userName = User.GetUsername();

            var user = await _userManager.FindByNameAsync(userName);

            var isCurrPassCorrect = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);

            if (!isCurrPassCorrect)
                return BadRequest("Invalid Current Password");

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.Password);

            if (!result.Succeeded) return BadRequest("Something went wrong");

            return Ok("Password changed successfully");
        }
    }
}
