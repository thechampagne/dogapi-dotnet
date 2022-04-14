/*
 * Copyright 2022 XXIV
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Dog
{
    /// <summary>
    /// Dog API client
    /// </summary>
    public class DogAPI
    {
        private static string GetRequest(string endpoint)
        {
            try
            {
                HttpWebRequest client = (HttpWebRequest) WebRequest.Create($"https://dog.ceo/api/{endpoint}");
                client.Method = "GET";
                var webResponse = client.GetResponse();
                var webStream = webResponse.GetResponseStream();
                var responseReader = new StreamReader(webStream);
                var response = responseReader.ReadToEnd();
                responseReader.Close();
                responseReader.Dispose();
                return response;
            }
            catch (WebException ex)
            {
                var webStream = ex.Response.GetResponseStream();
                var responseReader = new StreamReader(webStream);
                var response = responseReader.ReadToEnd();
                responseReader.Close();
                responseReader.Dispose();
                return response;
            }
        }

        /// <summary>
        /// DISPLAY SINGLE RANDOM IMAGE FROM ALL DOGS COLLECTION
        /// </summary>
        /// <returns>random dog image</returns>
        /// <exception cref="DogAPIException"></exception>
        public static string RandomImage()
        {
            try
            {
                var response = GetRequest("breeds/image/random");
                JObject data = JObject.Parse(response);
                if (data["status"].ToString() != "success")
                {
                    throw new Exception(data["message"].ToString());
                }

                return data["message"].ToString();
            }
            catch (JsonReaderException ex)
            {
                throw new DogAPIException("Something went wrong while reading json: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new DogAPIException(ex.Message);
            }
        }

        /// <summary>
        /// DISPLAY MULTIPLE RANDOM IMAGES FROM ALL DOGS COLLECTION
        /// </summary>
        /// <param name="imagesNumber">number of images</param>
        /// <returns>multiple random dog image `NOTE` ~ Max number returned is 50</returns>
        /// <exception cref="DogAPIException"></exception>
        public static List<string> MultipleRandomImages(int imagesNumber)
        {
            try
            {
                var response = GetRequest($"breeds/image/random/{imagesNumber}");
                JObject data = JObject.Parse(response);
                if (data["status"].ToString() != "success")
                {
                    throw new Exception(data["message"].ToString());
                }

                List<String> list = new List<string>();
                foreach (JValue i in data["message"])
                {
                    list.Add(i.ToString());
                }

                return list;
            }
            catch (JsonReaderException ex)
            {
                throw new DogAPIException("Something went wrong while reading json: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new DogAPIException(ex.Message);
            }
        }

        /// <summary>
        /// RANDOM IMAGE FROM A BREED COLLECTION
        /// </summary>
        /// <param name="breed">breed name</param>
        /// <returns>random dog image from a breed, e.g. hound</returns>
        /// <exception cref="DogAPIException"></exception>
        public static string RandomImageByBreed(string breed)
        {
            try
            {
                var response = GetRequest($"breed/{breed.Trim()}/images/random");
                JObject data = JObject.Parse(response);
                if (data["status"].ToString() != "success")
                {
                    throw new Exception(data["message"].ToString());
                }

                return data["message"].ToString();
            }
            catch (JsonReaderException ex)
            {
                throw new DogAPIException("Something went wrong while reading json: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new DogAPIException(ex.Message);
            }
        }

        /// <summary>
        /// MULTIPLE IMAGES FROM A BREED COLLECTION
        /// </summary>
        /// <param name="breed">breed name</param>
        /// <param name="imagesNumber">number of images</param>
        /// <returns>multiple random dog image from a breed, e.g. hound</returns>
        /// <exception cref="DogAPIException"></exception>
        public static List<string> MultipleRandomImagesByBreed(string breed, int imagesNumber)
        {
            try
            {
                var response = GetRequest($"breed/{breed.Trim()}/images/random/{imagesNumber}");
                JObject data = JObject.Parse(response);
                if (data["status"].ToString() != "success")
                {
                    throw new Exception(data["message"].ToString());
                }

                List<String> list = new List<string>();
                foreach (JValue i in data["message"])
                {
                    list.Add(i.ToString());
                }

                return list;
            }
            catch (JsonReaderException ex)
            {
                throw new DogAPIException("Something went wrong while reading json: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new DogAPIException(ex.Message);
            }
        }

        /// <summary>
        /// ALL IMAGES FROM A BREED COLLECTION
        /// </summary>
        /// <param name="breed">breed name</param>
        /// <returns>list all the images from a breed, e.g. hound</returns>
        /// <exception cref="DogAPIException"></exception>
        public static List<string> ImagesByBreed(string breed)
        {
            try
            {
                var response = GetRequest($"breed/{breed.Trim()}/images");
                JObject data = JObject.Parse(response);
                if (data["status"].ToString() != "success")
                {
                    throw new Exception(data["message"].ToString());
                }

                List<String> list = new List<string>();
                foreach (JValue i in data["message"])
                {
                    list.Add(i.ToString());
                }

                return list;
            }
            catch (JsonReaderException ex)
            {
                throw new DogAPIException("Something went wrong while reading json: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new DogAPIException(ex.Message);
            }
        }

        /// <summary>
        /// SINGLE RANDOM IMAGE FROM A SUB BREED COLLECTION
        /// </summary>
        /// <param name="breed">breed name</param>
        /// <param name="subBreed">sub_breed name</param>
        /// <returns>random dog image from a sub-breed, e.g. Afghan Hound</returns>
        /// <exception cref="DogAPIException"></exception>
        public static string RandomImageBySubBreed(string breed, string subBreed)
        {
            try
            {
                var response = GetRequest($"breed/{breed.Trim()}/{subBreed.Trim()}/images/random");
                JObject data = JObject.Parse(response);
                if (data["status"].ToString() != "success")
                {
                    throw new Exception(data["message"].ToString());
                }

                return data["message"].ToString();
            }
            catch (JsonReaderException ex)
            {
                throw new DogAPIException("Something went wrong while reading json: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new DogAPIException(ex.Message);
            }
        }

        /// <summary>
        /// MULTIPLE IMAGES FROM A SUB-BREED COLLECTION
        /// </summary>
        /// <param name="breed">breed name</param>
        /// <param name="subBreed">sub_breed name</param>
        /// <param name="imagesNumber">number of images</param>
        /// <returns>multiple random dog images from a sub-breed, e.g. Afghan Hound</returns>
        /// <exception cref="DogAPIException"></exception>
        public static List<string> MultipleRandomImagesBySubBreed(string breed, string subBreed, int imagesNumber)
        {
            try
            {
                var response = GetRequest($"breed/{breed.Trim()}/{subBreed.Trim()}/images/random/{imagesNumber}");
                JObject data = JObject.Parse(response);
                if (data["status"].ToString() != "success")
                {
                    throw new Exception(data["message"].ToString());
                }

                List<String> list = new List<string>();
                foreach (JValue i in data["message"])
                {
                    list.Add(i.ToString());
                }

                return list;
            }
            catch (JsonReaderException ex)
            {
                throw new DogAPIException("Something went wrong while reading json: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new DogAPIException(ex.Message);
            }
        }

        /// <summary>
        /// LIST ALL SUB-BREED IMAGES
        /// </summary>
        /// <param name="breed">breed name</param>
        /// <param name="subBreed">sub_breed name</param>
        /// <returns>list of all the images from the sub-breed</returns>
        /// <exception cref="DogAPIException"></exception>
        public static List<string> ImagesBySubBreed(string breed, string subBreed)
        {
            try
            {
                var response = GetRequest($"breed/{breed.Trim()}/{subBreed.Trim()}/images");
                JObject data = JObject.Parse(response);
                if (data["status"].ToString() != "success")
                {
                    throw new Exception(data["message"].ToString());
                }

                List<String> list = new List<string>();
                foreach (JValue i in data["message"])
                {
                    list.Add(i.ToString());
                }

                return list;
            }
            catch (JsonReaderException ex)
            {
                throw new DogAPIException("Something went wrong while reading json: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new DogAPIException(ex.Message);
            }
        }

        /// <summary>
        /// LIST ALL BREEDS
        /// </summary>
        /// <returns>dictionary of all the breeds as keys and sub-breeds as values if it has</returns>
        /// <exception cref="DogAPIException"></exception>
        public static IDictionary<string, List<string>> BreedsList()
        {
            try
            {
                var response = GetRequest("breeds/list/all");
                JObject data = JObject.Parse(response);
                if (data["status"].ToString() != "success")
                {
                    throw new Exception(data["message"].ToString());
                }

                IDictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
                foreach (JProperty i in data["message"])
                {
                    var key = i.Name;
                    List<string> list = new List<string>();
                    foreach (JArray value in i)
                    {
                        list.Add(value.ToString());
                    }

                    dictionary.Add(key, list);
                }

                return dictionary;
            }
            catch (JsonReaderException ex)
            {
                throw new DogAPIException("Something went wrong while reading json: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new DogAPIException(ex.Message);
            }
        }

        /// <summary>
        /// LIST ALL SUB-BREEDS
        /// </summary>
        /// <param name="breed">breed name</param>
        /// <returns>list of all the sub-breeds from a breed if it has sub-breeds</returns>
        /// <exception cref="DogAPIException"></exception>
        public static List<string> SubBreedsList(string breed)
        {
            try
            {
                var response = GetRequest($"breed/{breed.Trim()}/list");
                JObject data = JObject.Parse(response);
                if (data["status"].ToString() != "success")
                {
                    throw new Exception(data["message"].ToString());
                }

                List<String> list = new List<string>();
                foreach (JValue i in data["message"])
                {
                    list.Add(i.ToString());
                }

                if (list.Count == 0)
                {
                    throw new Exception("the breed does not have sub-breeds");
                }

                return list;
            }
            catch (JsonReaderException ex)
            {
                throw new DogAPIException("Something went wrong while reading json: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new DogAPIException(ex.Message);
            }
        }
    }
}