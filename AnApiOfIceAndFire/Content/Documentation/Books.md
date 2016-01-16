<a name="books"></a>
### Books


It has the following attributes:
<table class="table table-bordered table-hover">
  <thead>
    <tr>
      <th>Name</th>
      <th>Type</th>
      <th>Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>url</td>
      <td>string</td>
      <td>The hypermedia URL of this resource.</td>
    </tr>
    <tr>
      <td>name</td>
      <td>string</td>
      <td>The name of this book.</td>
    </tr>
    <tr>
      <td>isbn</td>
      <td>string</td>
      <td>The International Standard Book Number that uniquely identifies this book. The format used is ISBN-13.</td>
    </tr>
    <tr>
      <td>authors</td>
      <td>array of strings</td>
      <td>An array of names of the authors that wrote this book.</td>
    </tr>
    <tr>
      <td>numberOfPages</td>
      <td>integer</td>
      <td>The number of pages in this book.</td>
    </tr>
    <tr>
      <td>publisher</td>
      <td>string</td>
      <td>The company that published this book.</td>
    </tr>
    <tr>
      <td>country</td>
      <td>string</td>
      <td>The country that this book was published in.</td>
    </tr>
    <tr>
      <td>mediaType</td>
      <td>string</td>
      <td>The type of media this book was released in. Possible values are: Hardback, Hardcover, GraphicNovel and Paperback.</td>
    </tr>
    <tr>
      <td>released</td>
      <td>date</td>
      <td>The date, in ISO 8601 format, that this book was released</td>
    </tr>
    <tr>
      <td>characters</td>
      <td>array of string</td>
      <td>An array of Character resource URLs that has been in this book.</td>
    </tr>
    <tr>
      <td>povCharacters</td>
      <td>An array of Character resource URLs that has had a POV-chapter in this book.</td>
      <td></td>
    </tr>
  </tbody>
</table>

#### List all books


**Example request:**
``` command-line
$ curl "http://www.anapioficeandfire.com/api/books"
```


**Example response:**
``` command-line
[
  {
    "url": "http://www.anapioficeandfire.com/api/books/1",
    "name": "A Game of Thrones",
    "isbn": "978-0553103540",
    "authors": [
      "George R. R. Martin"
    ],
    "numberOfPages": 694,
    "publisher": "Bantam Books",
    "country": "United States",
    "mediaType": "Hardcover",
    "released": "1996-08-01T00:00:00",
    "characters": [
      "http://www.anapioficeandfire.com/api/characters/2",
      ...
    ],
    "povCharacters": [
      "http://www.anapioficeandfire.com/api/characters/148",
      ...
    ]
  },
  {
    "url": "http://www.anapioficeandfire.com/api/books/2",
    "name": "A Clash of Kings",
    "isbn": "978-0553108033",
    "authors": [
      "George R. R. Martin"
    ],
    "numberOfPages": 768,
    "publisher": "Bantam Books",
    "country": "United States",
    "mediaType": "Hardcover",
    "released": "1999-02-02T00:00:00",
    "characters": [
      "http://www.anapioficeandfire.com/api/characters/2",
      ...
    ],
    "povCharacters": [
      "http://www.anapioficeandfire.com/api/characters/148",
      ...
    ]
  },
  ...
 ]
```

#### Get specific book


**Example request:**
``` command-line
$ curl "http://www.anapioficeandfire.com/api/books/1"
```

**Example response:**
``` command-line
{
  "url": "http://www.anapioficeandfire.com/api/books/1",
  "name": "A Game of Thrones",
  "isbn": "978-0553103540",
  "authors": [
    "George R. R. Martin"
  ],
  "numberOfPages": 694,
  "publisher": "Bantam Books",
  "country": "United States",
  "mediaType": "Hardcover",
  "released": "1996-08-01T00:00:00",
  "characters": [
    "http://www.anapioficeandfire.com/api/characters/2",
    ...
  ],
  "povCharacters": [
    "http://www.anapioficeandfire.com/api/characters/148",
    ...
  ]
}
```