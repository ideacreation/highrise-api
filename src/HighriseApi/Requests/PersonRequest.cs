﻿using System;
using System.Collections.Generic;
using System.Net;
using HighriseApi.ExtensionMethods;
using HighriseApi.Interfaces;
using RestSharp;

namespace HighriseApi.Requests
{
    public class PersonRequest : RequestBase, IPersonRequest
    {
        public PersonRequest(IRestClient client) : base(client) { }

        public IEnumerable<Person> Get(int? offset = null)
        {
            var url = offset.HasValue
                          ? String.Format("people.xml?n={0}", offset.Value)
                          : "people.xml";

            var response = _client.Execute<List<Person>>(new RestRequest(url, Method.Get));
            return response.Data;
        }

        public IEnumerable<Person> Get(DateTime startDate)
        {
            var url = String.Format("people.xml?since={0}", startDate.ToString("yyyyMMddHHmmss"));

            var response = _client.Execute<List<Person>>(new RestRequest(url, Method.Get));
            return response.Data;
        }

        public Person Get(int id)
        {
            var response = _client.Execute<Person>(new RestRequest(String.Format("people/{0}.xml", id), Method.Get));
            return response.Data;
        }

        public Person Create(Person person)
        {
            var request = new RestRequest("people.xml", Method.Post);
            request.AddXmlBody(person);

            var response = _client.Execute<Person>(request);
            return response.Data;
        }

        public Person Update(Person person)
        {
            var request = new RestRequest("people/{id}.xml?reload=true", Method.Put);
            request.AddParameter("id", person.Id, ParameterType.UrlSegment);
            request.AddXmlBody(person);

            var response = _client.Execute<Person>(request);
            return response.Data;
        }

        public bool Delete(int id)
        {
            var response = _client.Execute<Person>(new RestRequest(String.Format("people/{0}.xml", id), Method.Delete));
            return response.StatusCode == HttpStatusCode.OK;
        }

        public IEnumerable<Person> Search(string name, int? offset = null)
        {
            if (String.IsNullOrEmpty(name)) return Get(offset);

            var url = offset.HasValue
                          ? String.Format("people/search.xml?term={0}&n={1}", name, offset.Value)
                          : String.Format("people/search.xml?term={0}", name);

            var response = _client.Execute<List<Person>>(new RestRequest(url, Method.Get));
            return response.Data;
        }

        public IEnumerable<Person> Search(IDictionary<string, string> values, int? offset = null)
        {
            if (values == null || values.Count == 0) return Get(offset);

            var url = offset.HasValue
                ? String.Format("people/search.xml?n={0}&{1}", offset.Value, values.ToSearchQueryString())
                : String.Format("people/search.xml?{0}", values.ToSearchQueryString());

            var response = _client.Execute<List<Person>>(new RestRequest(url, Method.Get));
            return response.Data;
        }
    }
}
