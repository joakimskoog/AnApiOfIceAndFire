# Documentation
- - -

<a name="intro"></a>
### Introduction


Introductory text here...

<a name="current_version"></a>
### Current Version


The current version of the API is v0. 

<a name="root_endpoint"></a>
### Root Endpoint


Text about root endpoint here...

<a name="authentication"></a>
### Authentication


An Api of Ice And Fire is an open API. This means that no authentication is required to query the API for data. Since no authentication is required, there is only support for **GET**-ing data.

<a name="pagination"></a>
### Pagination


Text about pagination here...

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

<a name="books"></a>
### Books


Information about books resource here...

<a name="characters"></a>
### Characters


Information about characters resource here...

<a name="houses"></a>
### Houses


Information about houses resource here...
