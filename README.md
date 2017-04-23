[![Build Status](https://travis-ci.org/Smallxmac/ProjectScribe.svg?branch=master)](https://travis-ci.org/Smallxmac/ProjectScribe)
# ProjectScribe

## Main Function:
  Project Scribe is a flexible multi-user note taking application. Notes are saved remotely and accessed via REST API for universal client support in any language. Anonymous note takers are able to use and manipulate the API, however all of their notes must be made public. User that login have the ability to create private notes that are not visible to the public if they so choose to hide them. The features will be broken down into core, and secondary. Where core features must be implemented, but secondary might be implemented and switched off due to lack of testing. 
  
### Please visit the wiki for more information!!!!

### Core Features:
 - Ability to Add notes to general repository
 - Cross Platform Server
 - Ability to remove, edit, and update notes that are made via user.
 - Search Notes

### Secondary Features:
 - Flexible data storage options
 - User Privileges
    - Admin (able to view, edit, or remove public and private notes)
    - Member (able to view, edit, or remove own private notes and view public notes.)
 - Private note encryption based on user private key.

## Langue Consideration:
While Qt 5.8 was first investigated as an option, it soon showed large overhead to get a simple http server with the simple integration. Qt route can be done well, it would take time to create a wrapper for the httpserver class to effectively handle api controllers.  My next thought was for Golang, which I still believe to be the best application for this project. Golang is a functional language that is very fast and created with the intent to be used as web server backends while featuring high performance and parallelism. However, in the project guidelines it mentioned the use of Object Oriented language. C# using the new cross-platform .netcore framework was the next best option. The web API is built on their MVC ASP.NET standard which allowed for every easy integration with api endpoint controllers. Because the project guidelines never mentioned a web api for the note taking application the rule were not tailored to that example. A functional approach for the web server makes the most sense. 

## Third Party Libraries Used:
Newtonsoft.json
 - Newtonsoft.json is used as a high performance (de)serializer for json objects. While .netcore is the ability to handle json,          Newtonsoft.json is the standard for using json in an application.
  
Swashbuckle
 - Swashbuckle is a a Swagger generator. Swagger is a API testing and documentation page. Swashbuckle generated meta data of the controllers and shows how they are used and the response codes. For that reason I will not go into detail on how request are responded to because it is all documented in the code using http standard conventions and in swagger.
 - Swagger can be access from BASE_URL/swagger.
