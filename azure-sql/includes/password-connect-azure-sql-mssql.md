1. In Visual Studio Code, in the Azure explorer, right-click your App Service and select **Open in portal**.
1. Navigate to the **Configuration** page for your App Service. Under the **Application Settings** tab, create environment variables for each property in the following table, with your own values. 

    |Property|
    |--|
    |AZURE_SQL_SERVER|
    |AZURE_SQL_DATABASE|
    |AZURE_SQL_PORT|
    |AZURE_SQL_USER|
    |AZURE_SQL_PASSWORD|


> [!WARNING]
> Use caution when managing connection objects that contain secrets such as usernames, passwords, or access keys. These secrets shouldn't be committed to source control or placed in unsecure locations where they might be accessed by unintended users. For a real application in a production-grade Azure environment, you can store connection information in a secure location such as App Service configuration settings or Azure Key Vault. During local development, you'll generally connect to a local database that doesn't require storing secrets or connecting directly to Azure.