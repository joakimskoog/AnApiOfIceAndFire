<a name="characters"></a>
### Characters


A Character resource is an individual within the Ice And Fire universe. 

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
      <td>The name of this character.</td>
    </tr>
    <tr>
      <td>culture</td>
      <td>string</td>
      <td>The culture that this character belongs to.</td>
    </tr>
    <tr>
      <td>born</td>
      <td>string</td>
      <td>The year that this person was born</td>
    </tr>
    <tr>
      <td>died</td>
      <td>string</td>
      <td>The year that this person died.</td>
    </tr>
    <tr>
      <td>titles</td>
      <td>array of strings</td>
      <td>The titles that this character holds.</td>
    </tr>
    <tr>
      <td>aliases</td>
      <td>array of strings</td>
      <td>The aliases that this character goes by.</td>
    </tr>
    <tr>
      <td>father</td>
      <td>string</td>
      <td>The character resource URL of this character's father.</td>
    </tr>
    <tr>
      <td>mother</td>
      <td>string</td>
      <td>The character resource URL of this character's mother.</td>
    </tr>
    <tr>
      <td>spouse</td>
      <td>string</td>
      <td>The character resource URL of this character's spouse.</td>
    </tr>
    <tr>
      <td>allegiances</td>
      <td>array of strings</td>
      <td>An array of House resource URLs that this character is loyal to.</td>
    </tr>
    <tr>
      <td>books</td>
      <td>array of strings</td>
      <td>An array of Book resource URLs that this character has been in.</td>
    </tr>
    <tr>
      <td>povBooks</td>
      <td>array of strings</td>
      <td>An array of Book resource URLs that this character has had a POV-chapter in.</td>
    </tr>
    <tr>
      <td>tvSeries</td>
      <td>array of strings</td>
      <td>An array of names of the seasons of Game of Thrones that this character has been in.</td>
    </tr>
    <tr>
      <td>playedBy</td>
      <td>array of strings</td>
      <td>An array of actor names that has played this character in the TV show Game Of Thrones.</td>
    </tr>
  </tbody>
</table>

#### List all characters


**Example request:**
``` command-line
$ curl "http://www.anapioficeandfire.com/api/characters"
```


**Example response:**
``` command-line
[
  {
    "url": "http://www.anapioficeandfire.com/api/characters/1",
    "name": "",
    "culture": "Braavosi",
    "born": "",
    "died": "",
    "titles": [],
    "aliases": [
      "The Daughter of the Dusk"
    ],
    "father": "",
    "mother": "",
    "spouse": "",
    "allegiances": [],
    "books": [
      "http://www.anapioficeandfire.com/api/books/5"
    ],
    "povBooks": [],
    "tvSeries": [],
    "playedBy": []
  },
  {
    "url": "http://www.anapioficeandfire.com/api/characters/2",
    "name": "",
    "culture": "",
    "born": "",
    "died": "",
    "titles": [],
    "aliases": [
      "Hodor"
    ],
    "father": "",
    "mother": "",
    "spouse": "",
    "allegiances": [
      "http://www.anapioficeandfire.com/api/houses/362"
    ],
    "books": [
      "http://www.anapioficeandfire.com/api/books/1",
      ...
    ],
    "povBooks": [],
    "tvSeries": [
      "Season 1",
      "Season 2",
      "Season 3",
      "Season 4"
    ],
    "playedBy": [
      "Kristian Nairn"
    ]
  },
  ...
]
```

#### Get specific character


**Example request:**
``` command-line
$ curl "http://www.anapioficeandfire.com/api/characters/823"
```

www.anapioficeandfire.co
**Example response:**
``` command-line
{
  "url": "http://www.anapioficeandfire.com/api/characters/823",
  "name": "Petyr Baelish",
  "culture": "Valemen",
  "born": "In 268 AC, at the Fingers",
  "died": "",
  "titles": [
    "Master of coin (formerly)",
    "Lord Paramount of the Trident",
    "Lord of Harrenhal",
    "Lord Protector of the Vale"
  ],
  "aliases": [
    "Littlefinger"
  ],
  "father": "",
  "mother": "",
  "spouse": "http://www.anapioficeandfire.com/api/characters/688",
  "allegiances": [
    "http://www.anapioficeandfire.com/api/houses/10",
    "http://www.anapioficeandfire.com/api/houses/11"
  ],
  "books": [
    "http://www.anapioficeandfire.com/api/books/1",
    ...
  ],
  "povBooks": [],
  "tvSeries": [
    "Season 1",
    "Season 2",
    "Season 3",
    "Season 4",
    "Season 5"
  ],
  "playedBy": [
    "Aidan Gillen"
  ]
}
```
