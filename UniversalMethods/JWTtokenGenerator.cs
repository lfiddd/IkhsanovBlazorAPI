using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace IkhsanovAPI.UniversalMethods;

public class JWTtokenGenerator
{
    private readonly string _secretKey;
    
    public JWTtokenGenerator(IConfiguration configuration)   {     
        _secretKey =  configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key");  
    } 
    
    public string GenerateJwtToken(int userId, int roleId)
    {
        var claims = new Claim[]
        {
            new Claim("id_user", userId.ToString()),
            new Claim("id_role", roleId.ToString()),
            
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));  
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); 
        
        var token = new JwtSecurityToken  
        (  
            claims: claims,  
            signingCredentials: creds  
        );    
    
        return new JwtSecurityTokenHandler().WriteToken(token);  
    }
}