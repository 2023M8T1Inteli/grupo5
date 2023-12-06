using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Threading.Tasks;

public class MailgunEmailService
{
    private readonly RestClient _client;
    private readonly string _domain = "sandbox9ad8ff59c37e4dfc9471a5b03a8b97a3.mailgun.org";
    private readonly string apiKey = "d730336df181d0414b23484ffbf49b26-0a688b4a-3df24469";

    public MailgunEmailService()
    {
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
        request.AddParameter("text", $"Por favor, utilize o seguinte token para configurar sua senha: {token}");

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
