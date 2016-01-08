# Documentation
- - -

<a name="intro"></a>
### Introduction


Introductory text here...

<a name="current_version"></a>
### Current Version


The current version of the API is 0. 

<a name="root_endpoint"></a>
### Root Endpoint


Text about root endpoint here...

<a name="authentication"></a>
### Authentication


An Api of Ice And Fire is an open API. This means that no authentication is required to query the API for data. Since no authentication is required, there is only support for **GET**-ing data.

<a name="pagination"></a>
### Pagination


An Api of Ice And Fire provides a lot of data about the world of Westeros. To prevent our servers from getting cranky, the API will automatically paginate the responses. You will learn how to create requests with pagination parameters and consume the response-

**Things worth noting:**

1. Information about the pagination is included in [the Link header](http://tools.ietf.org/html/rfc5988)
2. Page numbering is 1-based
3. You can specify how many items you want to receive per page, the maximum is 50

**Constructing a request with pagination**

You specify which page you want to access with the `?page` parameter, if you don't provide the `?page` parameter the first page will be returned. You can also specify the size of the page with the `?pageSize` parameter, if you don't provide the `?pageSize` parameter the default size of **10** will be used.

Let's make a request for the first page of characters with a page size of 10. Since we're only interested in the pagination information we provide the `-I` parameter to say that we only care about the headers.

``` command-line
$ curl -I "http://www.anapioficeandfire.com/api/characters?page=1&pageSize=10"
```

**Here's the link header in the response:**

	Link: <http://www.anapioficeandfire.com/api/characters?page=2&pageSize=10>; rel="next",
	<http://www.anapioficeandfire.com/api/characters?page=1&pageSize=10>; rel="first",  
	<http://www.anapioficeandfire.com/api/characters?page=214&pageSize=10>; rel="last"


**The possible values in the link response header are:**

<table class="table table-bordered table-hover">
  <thead>
    <tr>
      <th>Name</th>
      <th>Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>next</td>
      <td>The link to the next page of results.</td>
    </tr>
	<tr>
      <td>prev</td>
      <td>The link to the previous page of results.</td>
    </tr>
	<tr>
      <td>first</td>
      <td>The link to the first page of results.</td>
    </tr>
	<tr>
      <td>last</td>
      <td>The link to the last page of results.</td>
    </tr>
  </tbody>
</table>

These links can then be used to navigate to other pages of results.

<a name="rate_limiting"></a>
### Rate Limiting


Text about rate limiting here...

<a name="versioning"></a>
### Versioning


Custom media types are used in An Api of Ice And Fire to let consumers choose which **version** of the data they wish to receive. This is done by adding the following type to the Accept header when you make a request. Note that media types are specific to resources, this allows them to change independently from each other.

<div class="alert">

**Important:** If a version is not specified in the request the default version will be used. The default version may change in the future and can thus break the consumer's application. Make sure to **always** request a specific version in the ```Accept``` header as shown in the example below.

</div>

You specify a version like so:

    application/vnd.anapioficeandfire+json; version=0

# Resources
- - -

