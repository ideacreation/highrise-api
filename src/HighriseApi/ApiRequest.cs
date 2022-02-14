﻿using System;
using HighriseApi.Requests;
using RestSharp;
using RestSharp.Authenticators;

namespace HighriseApi
{
    public class ApiRequest
    {
        private readonly string _username;
        private readonly string _authenticationToken;
        private readonly IRestClient _client;

        public AccountRequest AccountRequest { get { return new AccountRequest(_client); } }
        public CaseRequest CaseRequest { get { return new CaseRequest(_client); } }
        public DealRequest DealRequest { get { return new DealRequest(_client); } }
        public CompanyRequest CompanyRequest { get { return new CompanyRequest(_client); } }
        public PersonRequest PersonRequest { get { return new PersonRequest(_client); } }
        public TagRequest TagRequest { get { return new TagRequest(_client); } }
        public UserRequest UserRequest { get { return new UserRequest(_client); } }
        public TaskRequest TaskRequest { get { return new TaskRequest(_client); } }
        public DealCategoryRequest DealCategoryRequest { get { return new DealCategoryRequest(_client); } }
        public TaskCategoryRequest TaskCategoryRequest { get { return new TaskCategoryRequest(_client); } }
        public CommentRequest CommentRequest { get { return new CommentRequest(_client); } }
        public SubjectFieldRequest SubjectFieldRequest { get { return new SubjectFieldRequest(_client); } }
        public RecordingRequest RecordingRequest { get { return new RecordingRequest(_client); } }
        public NoteRequest NoteRequest { get { return new NoteRequest(_client); } }
        public EmailRequest EmailRequest { get { return new EmailRequest(_client); } }
        public GroupRequest GroupRequest { get { return new GroupRequest(_client); } }
        public MembershipRequest MembershipRequest { get { return new MembershipRequest(_client); } }
        public ApiRequest(string username, string authenticationToken)
        {
            _username = username;
            _authenticationToken = authenticationToken;
            _client = new RestClient
                {
                    BaseUrl = new Uri(String.Format("https://{0}.highrisehq.com", _username)),
                    Authenticator = new HttpBasicAuthenticator(_authenticationToken, "X")
                };
        }
    }
}
