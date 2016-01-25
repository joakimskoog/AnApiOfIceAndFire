# Documentation
- - -

<a name="intro"></a>
### Introduction


This documentation will help you familiarise yourself with the API and how to consume the different resources that are available. The documentation provides all information needed to get started and it also has educational examples for all resources.

If you're interested in using a native implementation, please take a look at the **Libraries** section.

<a name="current_version"></a>
### Current Version


The current version of the API is 1. 

<a name="authentication"></a>
### Authentication


An API of Ice And Fire is an open API. This means that no authentication is required to query the API for data. Since no authentication is required, there is only support for **GET**-ing data.

<a name="pagination"></a>
### Pagination


An API of Ice And Fire provides a lot of data about the world of Westeros. To prevent our servers from getting cranky, the API will automatically paginate the responses. You will learn how to create requests with pagination parameters and consume the response-

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

<table class="table table-striped table-hover">
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


To prevent malicious usage of our API we've a limit on the number of requests a given IP address can make to the API. This limit is set to 5000 requests per day. There should be no reason for hitting the limit if you implement proper caching strategies in your client. If you happen to hit the limit you'll receive a [403 Forbidden](https://en.wikipedia.org/wiki/HTTP_403) on all your requests for the remainder of the 24 hour time period.

<a name="caching"></a>
### Caching


To help our server and your client we provide a few ways for you to use caching. Each API response includes the [ETag-header](https://en.wikipedia.org/wiki/HTTP_ETag) and the Last-Modified header.
These headers can be used to ask our server if the data has changed or not. If the data has not changed you will receive an empty response body with a [304 Not Modified](https://tools.ietf.org/html/rfc7232#section-4.1).
If the data has changed you will receive a 200 with the updated data.

**To use the ETag, include the following header in your request:**
``` command-line
If-None-Match: "your_etag_here"
```


**To use Last-Modified, include the following header in your request:**
``` command-line
If-Modified-Since: "date_here"
```

We advice you to use the above mentioned caching strategies, this will increase the speed of your client as well as save us bandwidth.

<a name="versioning"></a>
### Versioning


Custom media types are used in An API of Ice And Fire to let consumers choose which **version** of the data they wish to receive. This is done by adding the following type to the Accept header when you make a request. Note that media types are specific to resources, this allows them to change independently from each other.

<div class="alert alert-dismissible alert-warning">

**Important:** If a version is not specified in the request the default version will be used. The default version may change in the future and can thus break the consumer's application. Make sure to **always** request a specific version in the ```Accept``` header as shown in the example below.

</div>

You specify a version like so:

    application/vnd.anapioficeandfire+json; version=1

# Resources
- - -

