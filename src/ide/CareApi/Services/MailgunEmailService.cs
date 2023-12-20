using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Threading.Tasks;

public class MailgunEmailService
{
    private readonly RestClient _client;
    private readonly string? _domain;
    private readonly string? apiKey;

    public MailgunEmailService()
    {

        _domain = Environment.GetEnvironmentVariable("MAILGUN_DOMAIN");

        if (_domain is null)
        {
            throw new ApplicationException("MAILGUN_DOMAIN environment variable not set");
        }

        apiKey = Environment.GetEnvironmentVariable("MAILGUN_API_KEY");

        if (apiKey is null)
        {
            throw new ApplicationException("MAILGUN_API_KEY environment variable not set");
        }

        var options = new RestClientOptions($"https://api.mailgun.net/v3/")
        {
            Authenticator = new HttpBasicAuthenticator("api", apiKey)
        };
        _client = new RestClient(options);
    }

    public async Task SendPasswordSetupEmailAsync(string toEmail, string token)
    {
        var request = new RestRequest($"{_domain}/messages", Method.Post);
        request.AddParameter("from", $"Portal do Terapeuta <noreply@{_domain}>");
        request.AddParameter("to", toEmail);
        request.AddParameter("subject", "Configuração de Senha");
        request.AddParameter("text", $"Por favor, acesse o seguinte link para ativar a sua conta: http://localhost:3000/activate?email={toEmail}&token={token}");

        var response = await _client.ExecuteAsync(request);
        if (!response.IsSuccessful)
        {
            // Log the full response for debugging purposes
            var fullErrorMessage = $"Erro ao enviar e-mail: {response.ErrorMessage}, " +
                                   $"Response Status Code: {response.StatusCode}, " +
                                   $"Response Content: {response.Content}";
            throw new ApplicationException(fullErrorMessage);
        }
    }

}
