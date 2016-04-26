<a name="houses"></a>
### Houses


A House is a house branch within the Ice And Fire universe.


<table class="table table-bordered table-striped table-hover">
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
``` command-line
[
  {
    "url": "http://www.anapioficeandfire.com/api/houses/1",
    "name": "House Algood",
    "region": "The Westerlands",
    "coatOfArms": "A golden wreath, on a blue field with a gold border(Azure, a garland of laurel within a bordure or)",
    "words": "",
    "titles": [],
    "seats": [],
    "currentLord": "",
    "heir": "",
    "overlord": "http://www.anapioficeandfire.com/api/houses/229",
    "founded": "",
    "founder": "",
    "diedOut": "",
    "ancestralWeapons": [],
    "cadetBranches": [],
    "swornMembers": []
  },
  {
    "url": "http://www.anapioficeandfire.com/api/houses/2",
    "name": "House Allyrion of Godsgrace",
    "region": "Dorne",
    "coatOfArms": "Gyronny Gules and Sable, a hand couped Or",
    "words": "No Foe May Pass",
    "titles": [],
    "seats": [
      "Godsgrace"
    ],
    "currentLord": "http://www.anapioficeandfire.com/api/characters/298",
    "heir": "http://www.anapioficeandfire.com/api/characters/1922",
    "overlord": "http://www.anapioficeandfire.com/api/houses/285",
    "founded": "",
    "founder": "",
    "diedOut": "",
    "ancestralWeapons": [],
    "cadetBranches": [],
    "swornMembers": [
      "http://www.anapioficeandfire.com/api/characters/1301",
      ...
    ]
  },
  ...
]
```

#### Filtering houses


There is also the possibility to include filter parameters in your request to the http://www.anapioficeandfire.com/api/houses endpoint. They are described below.

<table class="table table-bordered table-striped table-hover">
  <thead>
    <tr>
      <th>Usage</th>
      <th>Result</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>?name=name_here</td>
      <td>Only houses with the given name are included in the response</td>
    </tr>
    <tr>
      <td>?region=region_here</td>
      <td>Only houses that belong in the given region are included in the response.</td>
    </tr>
    <tr>
      <td>?words=words_here</td>
      <td>Only houses that has the given words are included in the response.</td>
    </tr>
    <tr>
      <td>?hasWords=true_or_false</td>
      <td>Only houses that have words are included in the response.</td>
    </tr>
    <tr>
      <td>?hasTitles=true_or_false</td>
      <td>Only houses that have titles are included in the response.</td>
    </tr>
    <tr>
      <td>?hasSeats=true_or_false</td>
      <td>Only houses that have seats are included in the response.</td>
    </tr>
    <tr>
      <td>?hasDiedOut=true_or_false</td>
      <td>Only houses that are extinct are included in the response.</td>
    </tr>
    <tr>
      <td>?hasAncestralWeapons=true_or_false</td>
      <td>Only houses that have ancestral weapons are included in the response.</td>
    </tr>
  </tbody>
</table>

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