<a name="houses"></a>
### Houses


A House resource is a house branch within the Ice And Fire universe.

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
      <td>The name of this house.</td>
    </tr>
    <tr>
      <td>region</td>
      <td>string</td>
      <td>The region that this house resides in.</td>
    </tr>
    <tr>
      <td>coatOfArms</td>
      <td>string</td>
      <td>Text describing the coat of arms of this house.</td>
    </tr>
    <tr>
      <td>words</td>
      <td>string</td>
      <td>The words of this house.</td>
    </tr>
    <tr>
      <td>titles</td>
      <td>array of strings</td>
      <td>The titles that this house holds.</td>
    </tr>
    <tr>
      <td>seats</td>
      <td>array of strings</td>
      <td>The seats that this house holds.</td>
    </tr>
    <tr>
      <td>currentLord</td>
      <td>string</td>
      <td>The Character resource URL of this house's current lord.</td>
    </tr>
    <tr>
      <td>heir</td>
      <td>string</td>
      <td>The Character resource URL of this house's heir.</td>
    </tr>
    <tr>
      <td>overlord</td>
      <td>string</td>
      <td>The House resource URL that this house answers to.</td>
    </tr>
    <tr>
      <td>founded</td>
      <td>string</td>
      <td>The year that this house was founded.</td>
    </tr>
    <tr>
      <td>founder</td>
      <td>string</td>
      <td>The Character resource URL that founded this house.</td>
    </tr>
    <tr>
      <td>diedOut</td>
      <td>string</td>
      <td>The year that this house died out.</td>
    </tr>
    <tr>
      <td>ancestralWeapons</td>
      <td>array of strings</td>
      <td>An array of names of the noteworthy weapons that this house owns.</td>
    </tr>
    <tr>
      <td>cadetBranches</td>
      <td>array of strings</td>
      <td>An array of House resource URLs that was founded from this house.</td>
    </tr>
    <tr>
      <td>swornMembers</td>
      <td>array of strings</td>
      <td>An array of Character resource URLs that are sworn to this house.</td>
    </tr>
  </tbody>
</table>

#### List all houses


**Example request:**
``` command-line
$ curl "http://www.anapioficeandfire.com/api/houses"
```



**Example response:**
Response here

#### Get specific house


**Example request:**
``` command-line
$ curl "http://www.anapioficeandfire.co/api/houses/10"
```

**Example response:**
``` command-line
{
  "url": "http://www.anapioficeandfire.com/api/houses/10",
  "name": "House Baelish of Harrenhal",
  "region": "The Riverlands",
  "coatOfArms": "A field of silver mockingbirds, on a green field(Vert, semé of mockingbirds argent)",
  "words": "",
  "titles": [
    "Lord Paramount of the Trident",
    "Lord of Harrenhal"
  ],
  "seats": [
    "Harrenhal"
  ],
  "currentLord": "http://www.anapioficeandfire.com/api/characters/823",
  "heir": "",
  "overlord": "http://www.anapioficeandfire.com/api/houses/16",
  "founded": "299 AC",
  "founder": "http://www.anapioficeandfire.com/api/characters/823",
  "diedOut": "",
  "ancestralWeapons": [],
  "cadetBranches": [],
  "swornMembers": [
    "http://www.anapioficeandfire.com/api/characters/651",
    "http://www.anapioficeandfire.com/api/characters/804",
    "http://www.anapioficeandfire.com/api/characters/823",
    "http://www.anapioficeandfire.com/api/characters/957",
    "http://www.anapioficeandfire.com/api/characters/970"
  ]
}
```