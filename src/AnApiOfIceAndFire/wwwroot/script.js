// contains functionality for rendering highlighted code examples for each resource in Documentation/Index.cshtml
const maybeAddForwardSlash = (resourceEndpoint) => {
    return resourceEndpoint ? `/${resourceEndpoint}` : resourceEndpoint;
}

const get_curl_code_snippet = (resourceEndpoint) => {
    resourceEndpoint = maybeAddForwardSlash(resourceEndpoint);
    return `$ curl "https://www.anapioficeandfire.com/api${resourceEndpoint}"`;
}

const get_python_code_snippet = (resourceName, resourceEndpoint) => {
    resourceEndpoint = maybeAddForwardSlash(resourceEndpoint);
    return `import logging\nimport requests\n\nAN_API_OF_ICE_AND_FIRE_BASE_URL = "https://www.anapioficeandfire.com/api"\n\ndef get_${resourceName}():\n    url = f"{AN_API_OF_ICE_AND_FIRE_BASE_URL}${resourceEndpoint}"\n    resp = requests.get(url)\n    if resp.status_code != 200:\n        logging.info(f"Error on request to {url}: {resp.text}")\n        return None\n    return resp.json()\n\n${resourceName} = get_${resourceName}()\nprint(${resourceName})`;
}

const get_js_code_snippet = (resourceName, varName, resourceEndpoint) => {
    resourceEndpoint = maybeAddForwardSlash(resourceEndpoint);
    return `const AN_API_OF_ICE_AND_FIRE_BASE_URL = "https://www.anapioficeandfire.com/api";\n\nconst get${resourceName} = async () => {\n  const url = \`\${AN_API_OF_ICE_AND_FIRE_BASE_URL}${resourceEndpoint}\`;\n  const resp = await fetch(url);\n  if (resp.status !== 200) {\n    console.log(\`Error on request to \${url}: \${resp.statusText}\`);\n    return null;\n  };\n  const data = await resp.json();\n  return data;\n};\n\nget${resourceName}().then(${varName} => {\n  console.log(${varName});\n});`;
}


const codeSnippets = {
    "root": {
        "bash": get_curl_code_snippet(""),
        "python": get_python_code_snippet("root", ""),
        "js": get_js_code_snippet("Root", "root", ""),
    },
    "allBooks": {
        "bash": get_curl_code_snippet("books"),
        "python": get_python_code_snippet("books", "books"),
        "js": get_js_code_snippet("Books", "books", "books"),
    },
    "specificBook": {
        "bash": get_curl_code_snippet("books/1"),
        "python": get_python_code_snippet("book", "books/1"),
        "js": get_js_code_snippet("Book", "book", "books/1"),
    },
    "allCharacters": {
        "bash": get_curl_code_snippet("characters"),
        "python": get_python_code_snippet("characters", "characters"),
        "js": get_js_code_snippet("Characters", "characters", "characters"),
    },
    "specificCharacter": {
        "bash": get_curl_code_snippet("characters/823"),
        "python": get_python_code_snippet("character", "characters/823"),
        "js": get_js_code_snippet("Character", "character", "characters/823"),
    },
    "allHouses": {
        "bash": get_curl_code_snippet("house"),
        "python": get_python_code_snippet("houses", "houses"),
        "js": get_js_code_snippet("Houses", "houses", "houses"),
    },
    "specificHouse": {
        "bash": get_curl_code_snippet("houses/10"),
        "python": get_python_code_snippet("house", "houses/10"),
        "js": get_js_code_snippet("House", "house", "houses/10"),
    }
}

const setCodeSnippetsText = (e) => {
    Object.keys(codeSnippets).forEach(k => {
        const codeElem = document.getElementById(`${k}-code`);
        const codeSnippet = codeSnippets[k][e.target.value];
        codeElem.innerHTML = codeSnippet;
        codeElem.className = `language-${e.target.value}`
        const dropDownElem = document.getElementById(`${k}-lang`);
        dropDownElem.value = e.target.value;
    });
    Prism.highlightAll();
}

const addOnChangeListener = () => {
    document.addEventListener("DOMContentLoaded", () => {
        Object.keys(codeSnippets).forEach(k => {
            const elem = document.getElementById(`${k}-lang`);
            elem.addEventListener("change", setCodeSnippetsText);
        });
        Prism.highlightAll();
    });
}

addOnChangeListener();
