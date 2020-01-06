using System.Collections.Generic;
using System.Linq;
using Amazon.Extensions.Configuration.SystemsManager.Internal;
using Amazon.SimpleSystemsManagement.Model;
using Moq;
using Xunit;

namespace Amazon.Extensions.Configuration.SystemsManager.Tests
{
    public class JsonConfigurationProviderTests
    {

        public JsonConfigurationProviderTests()
        {
        }

        [Fact]
        public void ProcessParametersTest()
        {
            var input = 
            @"
            {
                ""RootLevel"":""first"",
                ""SecondLevel"":
                    {
                        ""Key"":""second""
                    },
                ""Number"":1,
                ""Array"":
                    [
                        ""value1"",
                        ""value2""
                    ]
            }";

            //TODO Spilt this up into separate test cases.


            var parsed = JsonConfigurationParser.Parse(input);

            Assert.Contains(parsed, parameter => parameter.Equals(new KeyValuePair<string, string>("RootLevel", "first")));
            Assert.Contains(parsed, parameter => parameter.Equals(new KeyValuePair<string, string>("SecondLevel:Key", "second")));
            Assert.Contains(parsed, parameter => parameter.Equals(new KeyValuePair<string, string>("Number", "1")));
            Assert.Contains(parsed, parameter => parameter.Equals(new KeyValuePair<string, string>("Array:0", "value1")));
            Assert.Contains(parsed, parameter => parameter.Equals(new KeyValuePair<string, string>("Array:1", "value2")));



        }
    }
}