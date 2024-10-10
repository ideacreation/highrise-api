using System;
using HighriseApi.Requests;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.Xml;

namespace HighriseApi
{
    public class ApiRequest
    {
        private readonly string _username;
        private readonly string _authenticationToken;
        private readonly IRestClient _client;
        
        public CompanyRequest CompanyRequest { get { return new CompanyRequest(_client); } }
        public PersonRequest PersonRequest { get { return new PersonRequest(_client); } }
        public TagRequest TagRequest { get { return new TagRequest(_client); } }
        public UserRequest UserRequest { get { return new UserRequest(_client); } }
        public ApiRequest(string username, string authenticationToken)
        {
            _username = username;
            _authenticationToken = authenticationToken;
            _client = new RestClient(new RestClientOptions()
            {
                BaseUrl = new Uri($"https://{_username}.highrisehq.com"),
                Authenticator = new HttpBasicAuthenticator(_authenticationToken, "X")
            }, configureSerialization: s => s.UseXmlSerializer(null, null, true));
        }
    }
}
