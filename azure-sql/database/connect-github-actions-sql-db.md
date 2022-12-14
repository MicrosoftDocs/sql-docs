---
title: "Quickstart: Connect to Azure SQL Database with GitHub Actions"
description: Use Azure SQL from a GitHub Actions workflow
author: juliakm
ms.author: jukullam
ms.reviewer: wiassaf, mathoma
ms.date: 10/28/2022
ms.service: sql-database
ms.subservice: connect
ms.topic: quickstart
ms.custom:
  - "github-actions-azure"
  - "mode-other"
---

# Use GitHub Actions to connect to Azure SQL Database

Get started with [GitHub Actions](https://docs.github.com/en/actions) by using a workflow to deploy database updates to [Azure SQL Database](../azure-sql-iaas-vs-paas-what-is-overview.md). 

## Prerequisites

You will need:
- An Azure account with an active subscription. [Create an account for free](https://azure.microsoft.com/free/?WT.mc_id=A261C142F).
- A GitHub repository with a dacpac package (`Database.dacpac`). If you don't have a GitHub account, [sign up for free](https://github.com/join).  
- An Azure SQL Database.
    - [Quickstart: Create an Azure SQL Database single database](single-database-create-quickstart.md)
    - [How to create a dacpac package from your existing SQL Server Database](/sql/relational-databases/data-tier-applications/export-a-data-tier-application)

## Workflow file overview

A GitHub Actions workflow is defined by a YAML (.yml) file in the `/.github/workflows/` path in your repository. This definition contains the various steps and parameters that make up the workflow.

The file has two sections:

|Section  |Tasks  |
|---------|---------|
|**Authentication** | 1.1. Generate deployment credentials. |
|**Deploy** | 1. Deploy the database. |

## Generate deployment credentials

[!INCLUDE [include](~/../azure-sql/reusable-content/github-actions/generate-deployment-credentials.md)]

## Copy the SQL connection string

In the Azure portal, go to your Azure SQL Database and open **Settings** > **Connection strings**. Copy the **ADO.NET** connection string. Replace the placeholder values for `your_database` and `your_password`. The connection string will look similar to this output. 

```output
    Server=tcp:my-sql-server.database.windows.net,1433;Initial Catalog={your-database};Persist Security Info=False;User ID={admin-name};Password={your-password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```

You'll use the connection string as a GitHub secret. 

## Configure the GitHub secrets

[!INCLUDE [include](~/../azure-sql/reusable-content/github-actions/create-secrets-with-openid.md)]

## Add your workflow

1. Go to **Actions** for your GitHub repository. 

2. Select **Set up your workflow yourself**. 

2. Delete everything after the `on:` section of your workflow file. For example, your remaining workflow may look like this. 

    ```yaml
    name: CI

    on:
    push:
        branches: [ main ]
    pull_request:
        branches: [ main ]
    ```

1. Rename your workflow `SQL for GitHub Actions` and add the checkout and login actions. These actions will check out your site code and authenticate with Azure using the `AZURE_CREDENTIALS` GitHub secret you created earlier.

    # [Service principal](#tab/userlevel)

    ```yaml
    name: SQL for GitHub Actions

    on:
    push:
        branches: [ main ]
    pull_request:
        branches: [ main ]

    jobs:
    build:
        runs-on: windows-latest
        steps:
        - uses: actions/checkout@v1
        - uses: azure/login@v1
        with:
            creds: ${{ secrets.AZURE_CREDENTIALS }}
    ```

    # [OpenID Connect](#tab/openid)

    ```yaml
    name: SQL for GitHub Actions

    on:
    push:
        branches: [ main ]
    pull_request:
        branches: [ main ]

    jobs:
    build:
        runs-on: windows-latest
        steps:
        - uses: actions/checkout@v1
        - uses: azure/login@v1
        with:
            client-id: ${{ secrets.AZURE_CLIENT_ID }}
            tenant-id: ${{ secrets.AZURE_TENANT_ID }}
            subscription-id: ${{ secrets.AZURE_SUBSCRIPTION_ID }}
    ```
  
  ---

1. Use the Azure SQL Deploy action to connect to your SQL instance. You should have a dacpac package (`Database.dacpac`) at the root level of your repository. 

    ```yaml
    - uses: azure/sql-action@v2
      with:
        connection-string: ${{ secrets.AZURE_SQL_CONNECTION_STRING }}
        path: './Database.dacpac'
        action: 'Publish'
    ```

1. Complete your workflow by adding an action to logout of Azure. Here's the completed workflow. The file will appear in the `.github/workflows` folder of your repository.

    # [Service principal](#tab/userlevel)

    ```yaml
    name: SQL for GitHub Actions

    on:
    push:
        branches: [ main ]
    pull_request:
        branches: [ main ]


    jobs:
    build:
        runs-on: windows-latest
        steps:
        - uses: actions/checkout@v1
        - uses: azure/login@v1
        with:
            creds: ${{ secrets.AZURE_CREDENTIALS }}

    - uses: azure/sql-action@v2
      with:
        connection-string: ${{ secrets.AZURE_SQL_CONNECTION_STRING }}
        path: './Database.dacpac'
        action: 'Publish'

        # Azure logout 
    - name: logout
      run: |
         az logout
    ```

    # [OpenID Connect](#tab/openid)

    ```yaml
    name: SQL for GitHub Actions

    on:
    push:
        branches: [ main ]
    pull_request:
        branches: [ main ]


    jobs:
    build:
        runs-on: windows-latest
        steps:
        - uses: actions/checkout@v1
        - uses: azure/login@v1
        with:
            client-id: ${{ secrets.AZURE_CLIENT_ID }}
            tenant-id: ${{ secrets.AZURE_TENANT_ID }}
            subscription-id: ${{ secrets.AZURE_SUBSCRIPTION_ID }}
    - uses: azure/sql-action@v2
      with:
        connection-string: ${{ secrets.AZURE_SQL_CONNECTION_STRING }}
        path: './Database.dacpac'
        action: 'Publish'

        # Azure logout 
    - name: logout
      run: |
         az logout
    ```

    ---

## Review your deployment

1. Go to **Actions** for your GitHub repository. 

1. Open the first result to see detailed logs of your workflow's run. 
 
   :::image type="content" source="media/quickstart-sql-github-actions/github-actions-run-sql.png" alt-text="Log of GitHub actions run":::

## Clean up resources

When your Azure SQL database and repository are no longer needed, clean up the resources you deployed by deleting the resource group and your GitHub repository. 

## Next steps

> [!div class="nextstepaction"]
> [Learn about Azure and GitHub integration](/azure/developer/github/)
