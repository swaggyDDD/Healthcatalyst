namespace Catalyst.Core.Data.Migrations.Install
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Catalyst.Core.Models.Domain;
    using Catalyst.Core.Models.PropData;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// A utility class for seeding initial (install) data.
    /// </summary>
    internal static class SeedDataHelper
    {
        /// <summary>
        /// Gets the people install data.
        /// </summary>
        /// <returns>
        /// Collection of persons for install data.
        /// </returns>
        public static IEnumerable<Person> GetDefaultPeople()
        {

            //// FYI - this is a hack for the demo
            var codeBase = typeof(SeedDataHelper).Assembly.CodeBase;
            var uri = new Uri(codeBase);
            var path = uri.LocalPath;
            var pathToBin = Path.GetDirectoryName(path);

            // find the \\src
            var basePath = pathToBin.Substring(0, pathToBin.IndexOf("\\src", StringComparison.Ordinal));

            var file = $"{basePath}\\data\\seed-demo-data.json";

            var json = string.Empty;

            if (File.Exists(file))
            {
                json = File.ReadAllText(file);
            }

            if (json.IsNullOrWhiteSpace()) return Enumerable.Empty<Person>();

            var jobj = JsonConvert.DeserializeObject<JObject>(json);
            var values = jobj.SelectToken("data").Children().ToList();

            var imports = new List<Person>();

            foreach (var p in values.OrderBy(x => Guid.NewGuid()))
            {
                var props = p.Values<string>().ToArray();

                var now = DateTime.UtcNow;
                var person = new Person
                                 {
                                     FirstName = props[0],
                                     LastName = props[1],
                                     Birthday = DateTime.Parse(props[2]),
                                     Slug = $"{props[0]}-{props[1]}".ToLowerInvariant(),
                                     UpdateDate = now,
                                     CreateDate = now
                                 };

                var address = new Address
                {
                    Name = "Default Address",
                    Address1 = props[3],
                    Locality = props[4],
                    Region = props[5],
                    PostalCode = props[6],
                    CountryCode = props[7],
                    UpdateDate = now,
                    CreateDate = now
                };
                person.Addresses.Add(address);

                imports.Add(person);
            }

            // me
            var me = new Person { FirstName = "Rusty", LastName = "Swayne", Birthday = DateTime.Parse("8/6/1971"), Slug = "rusty-swayne", Watch = true, CreateDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow.AddSeconds(25) };
            var adr = new Address { Name = "Space Needle", Address1 = "400 Broad St", Locality = "Seattle", Region = "WA", PostalCode = "98109", CountryCode = "US", CreateDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow };

            me.Addresses.Add(adr);

            var interestValue = new InterestList
                    {
                    // "Family", "Travel", "Movies", "Skiing", "SCUBA Diving", "Food"
                    Values = new List<Interest>
                            {
                                new Interest { Title = "Family", Url = string.Empty },
                                new Interest { Title = "Skiing", Url = "https://www.mtbaker.us/" },
                                new Interest { Title = "Travel", Url = string.Empty },
                                new Interest { Title = "SCUBA Diving", Url = string.Empty },
                                new Interest { Title = "Cooking", Url = string.Empty } 
                            }
                    };

            // PROPERTIES (concept)

            var interest = new ExtendedProperty
            {
                ConverterAlias = Constants.ExtendedProperties.InterestListConverterAlias,
                UpdateDate = DateTime.UtcNow,
                CreateDate = DateTime.UtcNow
            };

            interest.Converter().SetValue(interestValue);


            var socialValue = new SocialLinks
            {
                Facebook = "https://www.facebook.com/rustyswayne",
                Twitter = "https://twitter.com/rustyswayne",
                LinkedIn = "https://www.linkedin.com/in/rustyswayne/"
            };

            var social = new ExtendedProperty
                {
                    ConverterAlias = Constants.ExtendedProperties.SocialLinksConverterAlias,
                    UpdateDate = DateTime.UtcNow,
                    CreateDate = DateTime.UtcNow
                };

            social.Converter().SetValue(socialValue);



            //// TODO remove hard coded ref to .Web proj
            var photoVal = new Photo { Src = "/media/placeholders/rss.jpg" };

            var photo = new ExtendedProperty
            {
                ConverterAlias = Constants.ExtendedProperties.PhotoConverterAlias,
                UpdateDate = DateTime.UtcNow,
                CreateDate = DateTime.UtcNow
            };

            photo.Converter().SetValue(photoVal);


            me.Properties.Add(interest);
            me.Properties.Add(social);
            me.Properties.Add(photo);

            imports.Add(me);

            return imports;            
        }
    }
}