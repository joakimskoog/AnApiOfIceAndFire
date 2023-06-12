﻿﻿/// <reference path="../../../src/AnApiOfIceAndFire/wwwroot/script.js" />

// TODO: figure out how to use JSDOM in this file to test the setCodeSnippetsText and addOnChangeListener functions


describe("test maybeAddForwardSlash function", function () {
    it("should add a forward slash when passed a non-empty string", function () {
        const s = maybeAddForwardSlash("books");
        expect(s).toEqual("/books");
    });

    it("should return an empty string when passed a string", function () {
        const s = maybeAddForwardSlash("");
        expect(s).toEqual("");
    });
});

describe("test code snippet functions", function () {
    it("should return snippet with valid cURL request", function () {
        const snippet1 = `$ curl "https://www.anapioficeandfire.com/api/books"`
        const snippet2 = get_curl_code_snippet("books");
        expect(snippet1).toEqual(snippet2);
    });

    it("should return snippet with valid python code", function () {
        const snippet = get_python_code_snippet("books", "books");
        expect(snippet).toEqual(pythonCodeSnippetBooksResource);
    });

    it("should return snippet with valid JavaScript code", function () {
        const snippet1 = `const AN_API_OF_ICE_AND_FIRE_BASE_URL = "https://www.anapioficeandfire.com/api";\n\nconst getBooks = async () => {\n  const url = \`\${AN_API_OF_ICE_AND_FIRE_BASE_URL}/books\`;\n  const resp = await fetch(url);\n  if (resp.status !== 200) {\n    console.log(\`Error on request to \${url}: \${resp.statusText}\`);\n    return null;\n  };\n  const data = await resp.json();\n  return data;\n};\n\ngetBooks().then(books => {\n  console.log(books);\n});`;
        const snippet2 = get_js_code_snippet("Books", "books", "books")
        expect(snippet1).toEqual(snippet2);
    });
})


//describe("", function () {

//    beforeEach(function () {
//        document.body.innerHTML = documentationPageHTMLStr;
//    });

//    it("should set element's innerHTML to code snippets", function () {
//        setCodeSnippetsText(mockEvent);
//        const elem = document.getElementById("books-code");
//        expect(elem.innerHTML).toEqual(pythonCodeSnippetBooksResource);
//    });

//    it("should set element's class name to language-python", function () {
//        setCodeSnippetsText(mockEvent);
//        const elem = document.getElementById("books-code");
//        expect(elem.className).toEqual("language-python");
//    });

//    it("should dropdown language selection's value to python", function () {
//        setCodeSnippetsText(mockEvent);
//        const elem = document.getElementById("books-lang");
//        expect(elem.value).toEqual(mockEvent.target.value);
//    });
//})


const pythonCodeSnippetBooksResource = `import logging\nimport requests\n\nAN_API_OF_ICE_AND_FIRE_BASE_URL = "https://www.anapioficeandfire.com/api"\n\ndef get_books():\n    url = f"{AN_API_OF_ICE_AND_FIRE_BASE_URL}/books"\n    resp = requests.get(url)\n    if resp.status_code != 200:\n        logging.info(f"Error on request to {url}: {resp.text}")\n        return None\n    return resp.json()\n\nbooks = get_books()\nprint(books)`;

//const mockEvent = { target: { value: "python" } };

//const documentationPageHTMLStr = `<div id="layout-main - container" class="row">
//    < div id = "layout-content" class="large-12 columns" >
//        <section id="content" class="row">
//            <div class="large-12 columns">
//                <article>
//                    <header id="page-header">
//                        <h1 class="page-title">Documentation</h1>
//                    </header>
//                    <div class="row">
//                        <div class="small-12 column show-for-small-only">
//                            @Html.Partial("_Menu")
//                        </div>
//                        <div class="small-12 medium-8 column">
//                            <hr />
//                            <div>
//                                <a name="intro"></a>
//                                <h4>Introduction</h4>
//                                <p>This documentation will help you familiarise yourself with the API and how to consume the different resources that are available. The documentation provides all information needed to get started and it also has educational examples for all resources.</p>
//                                <p>
//                                    If you're interested in using a native implementation, please take a look at the <strong>Libraries</strong> section.
//                                </p>
//                            </div>
//                            <div>
//                                <a name="current-version"></a>
//                                <h4>Current Version</h4>
//                                <p>The current version of the API is 1.</p>
//                            </div>
//                            <div>
//                                <a name="authentication"></a>
//                                <h4>Authentication</h4>
//                                <p>An API of Ice And Fire is an open API. This means that no authentication is required to query the API for data. Since no authentication is required, there is only support for GET-ing data.</p>
//                            </div>
//                            <div>
//                                <a name="pagination"></a>
//                                <h4>Pagination</h4>
//                                <p>An API of Ice And Fire provides a lot of data about the world of Westeros. To prevent our servers from getting cranky, the API will automatically paginate the responses. You will learn how to create requests with pagination parameters and consume the response.</p>

//                                <h5>Things worth noting:</h5>

//                                <ol>
//                                    <li>Information about the pagination is included in <a href="https://tools.ietf.org/html/rfc5988">the Link header</a></li>
//                                    <li>Page numbering is 1-based</li>
//                                    <li>You can specify how many items you want to receive per page, the maximum is 50</li>
//                                </ol>

//                                <h5>Constructing a request with pagination</h5>
//                                <p>
//                                    You specify which page you want to access with the <code>?page</code> parameter, if you don't provide the <code>?page</code> parameter the first page will be returned. You can also specify the size of the page with the <code>?pageSize</code> parameter, if you don't provide the <code>?pageSize</code> parameter the default size of 10 will be used.
//                                </p>
//                                <p>Let's make a request for the first page of characters with a page size of 10. Since we're only interested in the pagination information we provide the -I parameter to say that we only care about the headers.</p>

//                                <code>$ curl -I "https://www.anapioficeandfire.com/api/characters?page=1&pageSize=10"</code>
//                                <p>
//                                    <strong>Here's the link header in the response:</strong>
//                                </p>
//                                <code>

//                                    Link: &lt;https://www.anapioficeandfire.com/api/characters?page=2&amp;pageSize=10&gt;; rel="next",
//                                    &lt;https://www.anapioficeandfire.com/api/characters?page=1&amp;pageSize=10&gt;; rel="first",
//                                    &lt;https://www.anapioficeandfire.com/api/characters?page=214&amp;pageSize=10&gt;; rel="last"
//                                </code>
//                                <p></p>
//                                <h5>Possible link types:</h5>
//                                <ul>
//                                    <li>
//                                        <strong>next</strong> - Next page of results
//                                    </li>
//                                    <li>
//                                        <strong>prev</strong> - Previous page of results
//                                    </li>
//                                    <li>
//                                        <strong>first</strong> - First page of results
//                                    </li>
//                                    <li>
//                                        <strong>last</strong> - Last page of results
//                                    </li>
//                                </ul>

//                                <p>These links can then be used to navigate to other pages of results.</p>
//                            </div>
//                            <div>
//                                <a name="rate-limiting"></a>
//                                <h4>Rate Limiting</h4>
//                                <p>To prevent malicious usage of our API we've a limit on the number of requests a given IP address can make to the API. This limit is set to 20000 requests per day. There should be no reason for hitting the limit if you implement proper caching strategies in your client. If you happen to hit the limit you'll receive a 403 Forbidden on all your requests for the remainder of the 24 hour time period.</p>
//                            </div>
//                            <div>
//                                <a name="caching"></a>
//                                <h4>Caching</h4>
//                                <p>Apart from the standard cache headers such as max-age we also provide a few different ways to ease the load on our servers and your client. Each API response includes the ETag-header and the Last-Modified header. These headers can be used to ask our server if the data has changed or not. If the data has not changed you will receive an empty response body with a 304 Not Modified. If the data has changed you will receive a 200 with the updated data.</p>

//                                <h5>To use the ETag, include the following header in your request:</h5>
//                                <code>If-None-Match: "your_etag_here"</code>
//                                <p></p>
//                                <h5>To use Last-Modified, include the following header in your request:</h5>
//                                <code>If-Modified-Since: "date_here"</code>
//                                <p></p>
//                            </div>
//                            <div>
//                                <a name="versioning"></a>
//                                <h4>Versioning</h4>
//                                <p>Custom media types are used in An API of Ice And Fire to let consumers choose which version of the data they wish to receive. This is done by adding the following type to the Accept header when you make a request. Note that media types are specific to resources, this allows them to change independently from each other.</p>

//                                <p>
//                                    <strong>Important:</strong> If a version is not specified in the request the default version will be used. The default version may change in the future and can thus break the consumer's application. Make sure to always request a specific version in the <code>Accept</code> header as shown in the example below.
//                                </p>

//                                <h5>You specify a version like so:</h5>
//                                <code>application/vnd.anapioficeandfire+json; version=1</code>
//                                <p></p>
//                            </div>



//                            <h3>Resources</h3>
//                            <hr />
//                            <div>
//                                <a name="root"></a>
//                                <h4>Root</h4>
//                                <p>The Root resource contains information about all available resources in the API.</p>
//                                <h5>Example request:</h5>
//                                <select id="root-lang">
//                                    <option value="bash">cURL</option>
//                                    <option value="python">Python</option>
//                                    <option value="js">JavaScript</option>
//                                </select>
//                                <pre>
//                                    <code class="language-bash line-numbers" id="root-code">$ curl "https://www.anapioficeandfire.com/api"</code>
//                                </pre>
//                                <h5>Example response:</h5>
//                                <pre><code class="language-json line-numbers">{
//                                    "books": "https://www.anapioficeandfire.com/api/books",
//                                    "characters": "https://www.anapioficeandfire.com/api/characters",
//                                    "houses": "https://www.anapioficeandfire.com/api/houses"
// }
//                                </code></pre>
//                                <p></p>
//                                <div>
//                                    <a name="books"></a>
//                                    <h4>Books</h4>
//                                    <table>
//                                        <thead>
//                                            <tr>
//                                                <th>Name</th>
//                                                <th>Type</th>
//                                                <th>Description</th>
//                                            </tr>
//                                        </thead>
//                                        <tbody>
//                                            <tr>
//                                                <td>url</td>
//                                                <td>string</td>
//                                                <td>The hypermedia URL of this resource</td>
//                                            </tr>
//                                            <tr>
//                                                <td>name</td>
//                                                <td>string</td>
//                                                <td>The name of this book</td>
//                                            </tr>
//                                            <tr>
//                                                <td>isbn</td>
//                                                <td>string</td>
//                                                <td>The International Standard Book Number (ISBN-13) that uniquely identifies this book.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>authors</td>
//                                                <td>array of strings</td>
//                                                <td>An array of names of the authors that wrote this book.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>numberOfPages</td>
//                                                <td>integer</td>
//                                                <td>The number of pages in this book.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>publiser</td>
//                                                <td>string</td>
//                                                <td>The company that published this book.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>country</td>
//                                                <td>string</td>
//                                                <td>The country that this book was published in</td>
//                                            </tr>
//                                            <tr>
//                                                <td>mediaType</td>
//                                                <td>string</td>
//                                                <td>The type of media this book was released in.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>released</td>
//                                                <td>date</td>
//                                                <td>The date (ISO 8601) when this book was released.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>characters</td>
//                                                <td>array of strings</td>
//                                                <td>An array of Character resource URLs that has been in this book.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>povCharacters</td>
//                                                <td>array of strings</td>
//                                                <td>An array of Character resource URLs that has had a POV-chapter in this book.</td>
//                                            </tr>
//                                        </tbody>
//                                    </table>


//                                    <h5>List all books:</h5>
//                                    <select id="allBooks-lang">
//                                        <option value="bash">cURL</option>
//                                        <option value="python">Python</option>
//                                        <option value="js">JavaScript</option>
//                                    </select>
//                                    <pre>
//                                        <code class="language-bash line-numbers" id="allBooks-code">$ curl "https://www.anapioficeandfire.com/api/books"</code>
//                                    </pre>
//                                    <p>
//                                        <strong>Example response:</strong>
//                                    </p>
//                                    <pre><code class="language-json line-numbers">[
//                                        {
//                                            "url": "https://www.anapioficeandfire.com/api/books/1",
//                                        "name": "A Game of Thrones",
//                                        "isbn": "978-0553103540",
//                                        "authors": [
//                                        "George R. R. Martin"
//                                        ],
//                                        "numberOfPages": 694,
//                                        "publisher": "Bantam Books",
//                                        "country": "United States",
//                                        "mediaType": "Hardcover",
//                                        "released": "1996-08-01T00:00:00",
//                                        "characters": [
//                                        "https://www.anapioficeandfire.com/api/characters/2",
//                                        ...
//                                        ],
//                                        "povCharacters": [
//                                        "https://www.anapioficeandfire.com/api/characters/148",
//                                        ...
//                                        ]
//  },
//                                        {
//                                            "url": "https://www.anapioficeandfire.com/api/books/2",
//                                        "name": "A Clash of Kings",
//                                        "isbn": "978-0553108033",
//                                        "authors": [
//                                        "George R. R. Martin"
//                                        ],
//                                        "numberOfPages": 768,
//                                        "publisher": "Bantam Books",
//                                        "country": "United States",
//                                        "mediaType": "Hardcover",
//                                        "released": "1999-02-02T00:00:00",
//                                        "characters": [
//                                        "https://www.anapioficeandfire.com/api/characters/2",
//                                        ...
//                                        ],
//                                        "povCharacters": [
//                                        "https://www.anapioficeandfire.com/api/characters/148",
//                                        ...
//                                        ]
//  },
//                                        ...
//                                        ]</code></pre>
//                                    <p></p>
//                                    <h5>Filtering books</h5>
//                                    <p>You can also include filter parameters in your request to the books endpoint by including parameters in the query string.</p>
//                                    <table>
//                                        <thead>
//                                            <tr>
//                                                <th>Parameter</th>
//                                                <th>Type</th>
//                                                <th>Result</th>
//                                            </tr>
//                                        </thead>
//                                        <tbody>
//                                            <tr>
//                                                <td>name</td>
//                                                <td>string</td>
//                                                <td>Books with the given name are included in the response</td>
//                                            </tr>
//                                            <tr>
//                                                <td>fromReleaseDate</td>
//                                                <td>date</td>
//                                                <td>Books that were released after, or on, the given date are included in the response.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>toReleaseDate</td>
//                                                <td>date</td>
//                                                <td>Books that were released before, or on, the given date are included in the response.</td>
//                                            </tr>
//                                        </tbody>
//                                    </table>

//                                    <h5>Get specific book</h5>
//                                    <select id="specificBook-lang">
//                                        <option value="bash">cURL</option>
//                                        <option value="python">Python</option>
//                                        <option value="js">JavaScript</option>
//                                    </select>
//                                    <pre>
//                                        <code class="language-bash line-numbers" id="specificBook-code">$ curl "https://www.anapioficeandfire.com/api/books/1"</code>
//                                    </pre>
//                                    <p>
//                                        <strong>Example response:</strong>
//                                    </p>
//                                    <pre><code class="language-json line-numbers">{
//                                        "url": "https://www.anapioficeandfire.com/api/books/1",
//                                        "name": "A Game of Thrones",
//                                        "isbn": "978-0553103540",
//                                        "authors": [
//                                        "George R. R. Martin"
//                                        ],
//                                        "numberOfPages": 694,
//                                        "publisher": "Bantam Books",
//                                        "country": "United States",
//                                        "mediaType": "Hardcover",
//                                        "released": "1996-08-01T00:00:00",
//                                        "characters": [
//                                        "https://www.anapioficeandfire.com/api/characters/2",
//                                        ...
//                                        ],
//                                        "povCharacters": [
//                                        "https://www.anapioficeandfire.com/api/characters/148",
//                                        ...
//                                        ]
//}</code></pre>
//                                </div>

//                                <p></p>
//                                <div>
//                                    <a name="characters"></a>
//                                    <h4>Characters</h4>
//                                    <p>A Character is an individual within the Ice And Fire universe.</p>
//                                    <table>
//                                        <thead>
//                                            <tr>
//                                                <th>Name</th>
//                                                <th>Type</th>
//                                                <th>Description</th>
//                                            </tr>
//                                        </thead>
//                                        <tbody>
//                                            <tr>
//                                                <td>url</td>
//                                                <td>string</td>
//                                                <td>The hypermedia URL of this resource</td>
//                                            </tr>
//                                            <tr>
//                                                <td>name</td>
//                                                <td>string</td>
//                                                <td>The name of this character</td>
//                                            </tr>
//                                            <tr>
//                                                <td>gender</td>
//                                                <td>string</td>
//                                                <td>The gender of this character.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>culture</td>
//                                                <td>string</td>
//                                                <td>The culture that this character belongs to.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>born</td>
//                                                <td>string</td>
//                                                <td>Textual representation of when and where this character was born.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>died</td>
//                                                <td>string</td>
//                                                <td>Textual representation of when and where this character died.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>titles</td>
//                                                <td>array of strings</td>
//                                                <td>The titles that this character holds.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>aliases</td>
//                                                <td>array of strings</td>
//                                                <td>The aliases that this character goes by.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>father</td>
//                                                <td>string</td>
//                                                <td>The character resource URL of this character's father.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>mother</td>
//                                                <td>string</td>
//                                                <td>The character resource URL of this character's mother.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>spouse</td>
//                                                <td>string</td>
//                                                <td>An array of Character resource URLs that has had a POV-chapter in this book.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>allegiances</td>
//                                                <td>array of strings</td>
//                                                <td>An array of House resource URLs that this character is loyal to.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>books</td>
//                                                <td>array of strings</td>
//                                                <td>An array of Book resource URLs that this character has been in.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>povBooks</td>
//                                                <td>array of strings</td>
//                                                <td>An array of Book resource URLs that this character has had a POV-chapter in.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>tvSeries</td>
//                                                <td>array of strings</td>
//                                                <td>An array of names of the seasons of Game of Thrones that this character has been in.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>playedBy</td>
//                                                <td>array of strings</td>
//                                                <td>An array of actor names that has played this character in the TV show Game Of Thrones.</td>
//                                            </tr>
//                                        </tbody>
//                                    </table>
//                                    <h5>List all characters:</h5>
//                                    <select id="allCharacters-lang">
//                                        <option value="bash">cURL</option>
//                                        <option value="python">Python</option>
//                                        <option value="js">JavaScript</option>
//                                    </select>
//                                    <pre>
//                                        <code class="language-bash line-numbers" id="allCharacters-code">$ curl "https://www.anapioficeandfire.com/api/characters"</code>
//                                    </pre>
//                                    <p>
//                                        <strong>Example response:</strong>
//                                    </p>
//                                    <pre><code class="language-json line-numbers">[
//                                        {
//                                            "url": "https://www.anapioficeandfire.com/api/characters/1",
//                                        "name": "",
//                                        "culture": "Braavosi",
//                                        "born": "",
//                                        "died": "",
//                                        "titles": [],
//                                        "aliases": [
//                                        "The Daughter of the Dusk"
//                                        ],
//                                        "father": "",
//                                        "mother": "",
//                                        "spouse": "",
//                                        "allegiances": [],
//                                        "books": [
//                                        "https://www.anapioficeandfire.com/api/books/5"
//                                        ],
//                                        "povBooks": [],
//                                        "tvSeries": [],
//                                        "playedBy": []
//  },
//                                        {
//                                            "url": "https://www.anapioficeandfire.com/api/characters/2",
//                                        "name": "",
//                                        "culture": "",
//                                        "born": "",
//                                        "died": "",
//                                        "titles": [],
//                                        "aliases": [
//                                        "Hodor"
//                                        ],
//                                        "father": "",
//                                        "mother": "",
//                                        "spouse": "",
//                                        "allegiances": [
//                                        "https://www.anapioficeandfire.com/api/houses/362"
//                                        ],
//                                        "books": [
//                                        "https://www.anapioficeandfire.com/api/books/1",
//                                        ...
//                                        ],
//                                        "povBooks": [],
//                                        "tvSeries": [
//                                        "Season 1",
//                                        "Season 2",
//                                        "Season 3",
//                                        "Season 4"
//                                        ],
//                                        "playedBy": [
//                                        "Kristian Nairn"
//                                        ]
//  },
//                                        ...
//                                        ]</code></pre>
//                                    <p></p>
//                                    <h5>Filtering characters</h5>
//                                    <p>You can also include filter parameters in your request to the characters endpoint by including parameters in the query string.</p>
//                                    <table>
//                                        <thead>
//                                            <tr>
//                                                <th>Parameter</th>
//                                                <th>Type</th>
//                                                <th>Result</th>
//                                            </tr>
//                                        </thead>
//                                        <tbody>
//                                            <tr>
//                                                <td>name</td>
//                                                <td>string</td>
//                                                <td>Characters with the given name are included in the response.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>gender</td>
//                                                <td>string</td>
//                                                <td>Characters with the given gender are included in the response.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>culture</td>
//                                                <td>string</td>
//                                                <td>Characters with the given culture are included in the response.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>born</td>
//                                                <td>string</td>
//                                                <td>Characters that were born this given year are included in the response.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>died</td>
//                                                <td>string</td>
//                                                <td>Characters that died this given year are included in the response.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>isAlive</td>
//                                                <td>boolean</td>
//                                                <td>Characters that are alive or dead (depending on the value) are included in the response.</td>
//                                            </tr>
//                                        </tbody>
//                                    </table>


//                                    <h5>Get specific character</h5>
//                                    <select id="specificCharacter-lang">
//                                        <option value="bash">cURL</option>
//                                        <option value="python">Python</option>
//                                        <option value="js">JavaScript</option>
//                                    </select>
//                                    <pre>
//                                        <code class="language-bash line-numbers" id="specificCharacter-code">$ curl "https://www.anapioficeandfire.com/api/characters/823"</code>
//                                    </pre>
//                                    <p>
//                                        <strong>Example response:</strong>
//                                    </p>
//                                    <pre><code class="language-json line-numbers">{
//                                        "url": "https://www.anapioficeandfire.com/api/characters/823",
//                                        "name": "Petyr Baelish",
//                                        "culture": "Valemen",
//                                        "born": "In 268 AC, at the Fingers",
//                                        "died": "",
//                                        "titles": [
//                                        "Master of coin (formerly)",
//                                        "Lord Paramount of the Trident",
//                                        "Lord of Harrenhal",
//                                        "Lord Protector of the Vale"
//                                        ],
//                                        "aliases": [
//                                        "Littlefinger"
//                                        ],
//                                        "father": "",
//                                        "mother": "",
//                                        "spouse": "https://www.anapioficeandfire.com/api/characters/688",
//                                        "allegiances": [
//                                        "https://www.anapioficeandfire.com/api/houses/10",
//                                        "https://www.anapioficeandfire.com/api/houses/11"
//                                        ],
//                                        "books": [
//                                        "https://www.anapioficeandfire.com/api/books/1",
//                                        ...
//                                        ],
//                                        "povBooks": [],
//                                        "tvSeries": [
//                                        "Season 1",
//                                        "Season 2",
//                                        "Season 3",
//                                        "Season 4",
//                                        "Season 5"
//                                        ],
//                                        "playedBy": [
//                                        "Aidan Gillen"
//                                        ]
//}</code></pre>
//                                    <p></p>
//                                </div>
//                                <div>
//                                    <a name="houses"></a>
//                                    <h4>Houses</h4>
//                                    <p>A House is a house branch within the Ice And Fire universe.</p>
//                                    <table>
//                                        <thead>
//                                            <tr>
//                                                <th>Name</th>
//                                                <th>Type</th>
//                                                <th>Description</th>
//                                            </tr>
//                                        </thead>
//                                        <tbody>
//                                            <tr>
//                                                <td>url</td>
//                                                <td>string</td>
//                                                <td>The hypermedia URL of this resource</td>
//                                            </tr>
//                                            <tr>
//                                                <td>name</td>
//                                                <td>string</td>
//                                                <td>The name of this house</td>
//                                            </tr>
//                                            <tr>
//                                                <td>region</td>
//                                                <td>string</td>
//                                                <td>The region that this house resides in.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>coatOfArms</td>
//                                                <td>string</td>
//                                                <td>Text describing the coat of arms of this house.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>words</td>
//                                                <td>string</td>
//                                                <td>The words of this house.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>titles</td>
//                                                <td>array of strings</td>
//                                                <td>The titles that this house holds.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>seats</td>
//                                                <td>array of strings</td>
//                                                <td>The seats that this house holds.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>currentLord</td>
//                                                <td>string</td>
//                                                <td>The Character resource URL of this house's current lord.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>heir</td>
//                                                <td>string</td>
//                                                <td>The Character resource URL of this house's heir.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>overlord</td>
//                                                <td>string</td>
//                                                <td>The House resource URL that this house answers to.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>founded</td>
//                                                <td>string</td>
//                                                <td>The year that this house was founded.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>founder</td>
//                                                <td>string</td>
//                                                <td>The Character resource URL that founded this house.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>diedOut</td>
//                                                <td>string</td>
//                                                <td>The year that this house died out.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>ancestralWeapons</td>
//                                                <td>array of strings</td>
//                                                <td>An array of names of the noteworthy weapons that this house owns.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>cadetBranches</td>
//                                                <td>array of strings</td>
//                                                <td>An array of House resource URLs that was founded from this house.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>swornMembers</td>
//                                                <td>array of strings</td>
//                                                <td>An array of Character resource URLs that are sworn to this house.</td>
//                                            </tr>
//                                        </tbody>
//                                    </table>

//                                    <h5>List all houses:</h5>
//                                    <select id="allHouses-lang">
//                                        <option value="bash">cURL</option>
//                                        <option value="python">Python</option>
//                                        <option value="js">JavaScript</option>
//                                    </select>
//                                    <pre>
//                                        <code class="language-bash line-numbers" id="allHouses-code">$ curl "https://www.anapioficeandfire.com/api/houses"</code>
//                                    </pre>
//                                    <p>
//                                        <strong>Example response:</strong>
//                                    </p>
//                                    <pre><code class="language-json line-numbers">[
//                                        {
//                                            "url": "https://www.anapioficeandfire.com/api/houses/1",
//                                        "name": "House Algood",
//                                        "region": "The Westerlands",
//                                        "coatOfArms": "A golden wreath, on a blue field with a gold border",
//                                        "words": "",
//                                        "titles": [],
//                                        "seats": [],
//                                        "currentLord": "",
//                                        "heir": "",
//                                        "overlord": "https://www.anapioficeandfire.com/api/houses/229",
//                                        "founded": "",
//                                        "founder": "",
//                                        "diedOut": "",
//                                        "ancestralWeapons": [],
//                                        "cadetBranches": [],
//                                        "swornMembers": []
//  },
//                                        {
//                                            "url": "https://www.anapioficeandfire.com/api/houses/2",
//                                        "name": "House Allyrion of Godsgrace",
//                                        "region": "Dorne",
//                                        "coatOfArms": "Gyronny Gules and Sable, a hand couped Or",
//                                        "words": "No Foe May Pass",
//                                        "titles": [],
//                                        "seats": [
//                                        "Godsgrace"
//                                        ],
//                                        "currentLord": "https://www.anapioficeandfire.com/api/characters/298",
//                                        "heir": "https://www.anapioficeandfire.com/api/characters/1922",
//                                        "overlord": "https://www.anapioficeandfire.com/api/houses/285",
//                                        "founded": "",
//                                        "founder": "",
//                                        "diedOut": "",
//                                        "ancestralWeapons": [],
//                                        "cadetBranches": [],
//                                        "swornMembers": [
//                                        "https://www.anapioficeandfire.com/api/characters/1301",
//                                        ...
//                                        ]
//  },
//                                        ...
//                                        ]</code></pre>
//                                    <p></p>

//                                    <h5>Filtering houses</h5>
//                                    <p>You can also include filter parameters in your request to the characters endpoint by including parameters in the query string.</p>
//                                    <table>
//                                        <thead>
//                                            <tr>
//                                                <th>Parameter</th>
//                                                <th>Type</th>
//                                                <th>Result</th>
//                                            </tr>
//                                        </thead>
//                                        <tbody>
//                                            <tr>
//                                                <td>name</td>
//                                                <td>string</td>
//                                                <td>Houses with the given name are included in the response</td>
//                                            </tr>
//                                            <tr>
//                                                <td>region</td>
//                                                <td>string</td>
//                                                <td>Houses that belong in the given region are included in the response.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>words</td>
//                                                <td>string</td>
//                                                <td>Houses that has the given words are included in the response.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>hasWords</td>
//                                                <td>boolean</td>
//                                                <td>Houses that have words (or not) are included in the response.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>hasTitles</td>
//                                                <td>boolean</td>
//                                                <td>Houses that have titles (or not) are included in the response.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>hasSeats</td>
//                                                <td>boolean</td>
//                                                <td>Houses that have seats (or not) are included in the response.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>hasDiedOut</td>
//                                                <td>boolean</td>
//                                                <td>Houses that are extinct are included in the response.</td>
//                                            </tr>
//                                            <tr>
//                                                <td>hasAncestralWeapons</td>
//                                                <td>boolean</td>
//                                                <td>Houses that have ancestral weapons (or not) are included in the response.</td>
//                                            </tr>
//                                        </tbody>
//                                    </table>


//                                    <h5>Get specific house</h5>
//                                    <select id="specificHouse-lang">
//                                        <option value="bash">cURL</option>
//                                        <option value="python">Python</option>
//                                        <option value="js">JavaScript</option>
//                                    </select>
//                                    <pre>
//                                        <code class="language-bash line-numbers" id="specificHouse-code">$ curl "https://www.anapioficeandfire.com/api/houses/10"</code>
//                                    </pre>
//                                    <p>
//                                        <strong>Example response:</strong>
//                                    </p>
//                                    <pre><code class="language-json line-numbers">{
//                                        "url": "https://www.anapioficeandfire.com/api/houses/10",
//                                        "name": "House Baelish of Harrenhal",
//                                        "region": "The Riverlands",
//                                        "coatOfArms": "A field of silver mockingbirds, on a green field",
//                                        "words": "",
//                                        "titles": [
//                                        "Lord Paramount of the Trident",
//                                        "Lord of Harrenhal"
//                                        ],
//                                        "seats": [
//                                        "Harrenhal"
//                                        ],
//                                        "currentLord": "https://www.anapioficeandfire.com/api/characters/823",
//                                        "heir": "",
//                                        "overlord": "https://www.anapioficeandfire.com/api/houses/16",
//                                        "founded": "299 AC",
//                                        "founder": "https://www.anapioficeandfire.com/api/characters/823",
//                                        "diedOut": "",
//                                        "ancestralWeapons": [],
//                                        "cadetBranches": [],
//                                        "swornMembers": [
//                                        "https://www.anapioficeandfire.com/api/characters/651",
//                                        "https://www.anapioficeandfire.com/api/characters/804",
//                                        "https://www.anapioficeandfire.com/api/characters/823",
//                                        "https://www.anapioficeandfire.com/api/characters/957",
//                                        "https://www.anapioficeandfire.com/api/characters/970"
//                                        ]
//}</code></pre>
//                                    <p></p>
//                                </div>
//                            </div>

//                            @Html.Partial("_Libraries")
//                        </div>
//                        <div class="small-12 medium-4 column hide-for-small-only">
//                            @Html.Partial("_Menu")
//                        </div>
//                    </div>
//                </article>
//            </div>
//        </section>
//    </div >
//</div >`
