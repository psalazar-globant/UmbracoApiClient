# RoundTable V2 

## Description
UmbracoBridgeApi is an API that acts as a bridge to communicate with Umbraco.


### Prerequisites
Make sure you have [.NET 9.0](https://dotnet.microsoft.com/download) installed on your system.

### Steps to Run

1. **Open Command Line**:
   - Open a terminal or command prompt.

2. **Start the Umbraco Project**:
   - Navigate to the Umbraco project folder:
     ```bash
     cd path/to/your/project/Umbraco
     ```
   - Start the project:
     ```bash
     dotnet run
     ```
3. **Modify `appsettings.Development.json` for UmbracoBridgeApi**:
   - Before running the `UmbracoBridgeApi`, modify the `appsettings.Development.json` file to point to the running instance of Umbraco. Update the following settings:
   ```json
   "UmbracoSettings": {
       "BaseUrl": "https://localhost:44303", // Change here
       "ClientId": "umbraco-back-office-id",
       "ClientSecret": "secret",
       "TokenEndpoint": "https://localhost:44303/umbraco/management/api/v1/security/back-office/token", // Change here
       "ApiEndpoints": {
           "HealthCheckUrl": "/umbraco/management/api/v1/health-check-group",
           "PostDocumentTypeUrl": "/umbraco/management/api/v1/document-type",
           "DeleteDocumentUrl": "/umbraco/management/api/v1/document-type/"
       }
   }

4. **Open Another Command Line**:
   - Open a new terminal or command prompt.

5. **Start the Umbraco Project**:
   - Navigate to the Umbraco project folder:
     ```bash
     cd path/to/your/project/UmbracoBridgeApi
     ```
   - Start the project:
     ```bash
     dotnet run
     ```
### Steps to test
1. **Open the UmbracoBridgeAPi swagger page**:
   - Now that we have our two projects running go access the UmbradcoBridgeApi swagger page, under: `http://localhost:5000/umbracoBridge/swagger/index.html`

      Once you are in the Swagger UI, you can utilize the following endpoints:

1. **GET /api/UmbracoBridge/Healthcheck**

      You can call this endpoint without any parameters.

    It should return `200` status code with the following body:
   ```json
       {
           "total": 6,
           "items": [
              {"name": "Configuration"},
              {"name": "Data Integrity"},
              {"name": "Live Environment"},
              {"name": "Permissions"},
              {"name": "Security"},
              {"name": "Services"}
          ]
      }
2. **POST /api/UmbracoBridge/DocumentType**

      Call this endpoint with the following message:

      ```json
        {
            "total": 6,
            "items": [
               {"name": "Configuration"},
               {"name": "Data Integrity"},
               {"name": "Live Environment"},
               {"name": "Permissions"},
               {"name": "Security"},
               {"name": "Services"}
           ]
        }
      ```
   It should return `200` status code with the following body:
      ```json
      {
        "resourceIdentifier": "4584f8b6-fa2f-43d9-ad1b-5dee4f6d642c",
        "location": "https://localhost:44303/umbraco/management/api/v1/document-type/4584f8b6-fa2f-43d9-ad1b-5dee4f6d642c"
   }

3. **DELETE /api/UmbracoBridge/DocumentType/{id}**

      Call this endpoint with id obtained in the previous call

      You should get the following message: `Document 4584f8b6-fa2f-43d9-ad1b-5dee4f6d642c succesfully deleted.`

### Support
Please contact [@psalazar-globant](https://github.com/psalazar-globant) for any inquiries.
