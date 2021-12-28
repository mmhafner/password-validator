<h1 align="center">
	<!-- <img alt="Logo" src=".github/logo.png" width="200px" /> -->
  Password Validator
</h1>

<h3 align="center">
  Password Validator API
</h3>

<p align="center">  Ensure your password is secure enough</p>

<p align="center">
  <img alt="GitHub top language" src="https://img.shields.io/github/languages/top/mmhafner/password-validator">

  <a href="https://www.linkedin.com/in/eliasgcf/">
    <img alt="Made by" src="https://img.shields.io/badge/made%20by-Matheus%20Hafner-gree">
  </a>
  
  <img alt="Repository size" src="https://img.shields.io/github/repo-size/mmhafner/password-validator">
  
  <a href="https://github.com/mmhafner/password-validator/commits/master">
    <img alt="GitHub last commit" src="https://img.shields.io/github/last-commit/mmhafner/password-validator">
  </a>
  
  <a href="https://github.com/mmhafner/password-validator/issues">
    <img alt="Repository issues" src="https://img.shields.io/github/issues/mmhafner/password-validator">
  </a>
  
  <img alt="GitHub" src="https://img.shields.io/github/license/mmhafner/password-validator"> 
</p>

<p align="center">
  <a href="#-about-the-project">About the project</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#-technologies">Technologies</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#-rest-api">Rest API</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#-how-to-contribute">How to contribute</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#-license">License</a>
</p>

<p id="swaggerButton" align="center">
  <a href="https://mmhafner.github.io/password-validator" target="_blank"><img src="http://jessemillar.github.io/view-in-swagger-button/button.svg" alt="View in Swagger"></a>
</p>


## 👨🏻‍💻 About the project

Password validation API with the following rules:
- Nine or more characters
- At least one digit
- At least one lowercase letter
- At least one uppercase letter
- At least one special character **(Consider !@#$%^&*()-+ as special characters)**
- No repeated characters

## 🚀 Technologies

Technologies that I used to develop this api

- [.Net 5 ](https://docs.microsoft.com/pt-br/dotnet/core/whats-new/dotnet-5)
- [Swagger](https://swagger.io/)
- [xUnit](https://xunit.net/)
- [Moq.Automocker](https://github.com/moq/Moq.AutoMocker)
- [Fluent Assertions](https://fluentassertions.com/)


## 📄 REST API

The REST API to the password validator is described below.

### Password Validate

#### Request

`POST /PasswordValidator/`

    curl -i -H 'Accept: application/json' https://localhost:44355/PasswordValidator/
#### Request Body
`"string"` With the password to be validated

#### Response
##### Valid Password

###### Http Code
	   200 Success 
###### Response Header
	    content-encoding: gzip 
	    content-type: text/json; 
	    charset=utf-8  
	    date: Tue28 Dec 2021 18:05:23 GMT  
	    server: Microsoft-IIS/10.0  
	    vary: Accept-Encoding 
	    x-powered-by: ASP.NET
  ###### Response Body
```json
{
  "success": true,
  "errors": null
}
```
##### Invalid Password
###### Http Code
	   200 Success 
###### Response Header
	   content-length: 280 
       content-type: application/json; 
       charset=utf-8  
       date: Tue28 Dec 2021 17:57:10 GMT  
       server: Microsoft-IIS/10.0 x-powered-by: ASP.NET
  ###### Response Body
```json
{
  "success": false,
  "errors": {
    "0": "The password is less than 9 characters",
    "1": "The password has no digits",
    "2": "The password has no lowercase letter",
    "3": "The password has no uppercase letter",
    "4": "The password has no special characters",
    "5": "The password has repeated characters"
  }
}
```
##### Bad Request
###### Http Code
	   400 Bad Request
###### Response Header
	   content-type: application/problem+json; charset=utf-8
	   date: Tue28 Dec 2021 18:07:08 GMT  
	   server: Microsoft-IIS/10.0 
	   x-powered-by: ASP.NET
	   
  ###### Response Body
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "traceId": "00-6fd6ce4518090248b95f18ba8b38060e-0ec528b8ed959a44-00",
  "errors": {
    "": [
      "A non-empty request body is required."
    ]
  }
}
```

    
## 🤔 How to contribute

**Make a fork of this repository**

```bash
# Fork using GitHub official command line
# If you don't have the GitHub CLI, use the web site to do that.

$ gh repo fork mmhafner/password-validator
```

**Follow the steps below**

```bash
# Clone your fork
$ git clone your-fork-url && cd password-validator

# Create a branch with your feature
$ git checkout -b my-feature

# Make the commit with your changes
$ git commit -m 'feat: My new feature'

# Send the code to your remote branch
$ git push origin my-feature
```

After your pull request is merged, you can delete your branch

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

Made by Matheus Hafner 👋 &nbsp;[See my linkedin](https://www.linkedin.com/in/mmhafner/)