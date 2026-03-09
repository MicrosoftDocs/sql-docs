---
title: "Quickstart: Connect to database with GitHub Actions"
description: Use Azure SQL Database from a GitHub Actions workflow
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: wiassaf, mathoma
ms.date: 08/05/2025
ms.service: azure-sql-database
ms.subservice: connect
ms.topic: quickstart
ms.custom: github-actions-azure, mode-other, devx-track-azurecli
---

# Use GitHub Actions to connect to Azure SQL Database
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

Get started with [GitHub Actions](https://docs.github.com/en/actions) by using a workflow to deploy database updates to [Azure SQL Database](../azure-sql-iaas-vs-paas-what-is-overview.md). 

## Prerequisites

You need:
- An Azure account with an active subscription. [Create an account for free](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn).
- A GitHub repository with a dacpac package (`Database.dacpac`). If you don't have a GitHub account, [sign up for free](https://github.com/join).  
- An Azure SQL Database. [Quickstart: Create an Azure SQL Database single database](single-database-create-quickstart.md).
- A [.dacpac file](/sql/relational-databases/data-tier-applications/data-tier-applications#benefits-of-data-tier-applications) to import into your database.

## Workflow file overview

A GitHub Actions workflow is defined by a YAML (.yml) file in the `/.github/workflows/` path in your repository. This file has the steps and parameters that make up the workflow.

The file has two sections:

|Section  |Tasks  |
|---------|---------|
|**Authentication** | 1. Generate deployment credentials. |
|**Deploy** | 2. Deploy the database. |

## Generate deployment credentials

[!INCLUDE [include](~/../azure-sql/reusable-content/github-actions/generate-deployment-credentials.md)]

## Copy the SQL connection string

In the Azure portal, go to your Azure SQL Database and open **Settings** > **Connection strings**. Copy the **ADO.NET** connection string. Replace the placeholder values for `your_database` and `your_password`. 

You'll set the connection string as a GitHub secret, `AZURE_SQL_CONNECTION_STRING`.

## Configure the GitHub secrets

[!INCLUDE [include](~/../azure-sql/reusable-content/github-actions/create-secrets-with-openid.md)]

### Add the SQL connection string secret

1. In [GitHub](https://github.com/), go to your repository.

1. Go to **Settings** in the navigation menu.

1. Select **Security > Secrets and variables > Actions**.

1. Select **New repository secret**.

1. Paste your SQL connection string. Give the secret the name `AZURE_SQL_CONNECTION_STRING`.

1. Select **Add secret**.

## Add your workflow

1. Go to **Actions** for your GitHub repository.

2. Select **Set up your workflow yourself**.

3. Delete everything after the `on:` section of your workflow file. For example, your remaining workflow can look like this. 

    ```yaml
    name: SQL for GitHub Actions

    on:
        push:
            branches: [ main ]
        pull_request:
            branches: [ main ]
    ```

4. Rename your workflow `SQL for GitHub Actions` and add the checkout and login actions. These actions check out your site code and authenticate with Azure using the `AZURE_CREDENTIALS` GitHub secret you created earlier.

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
                 - uses: azure/login@v2
                   with:
                    client-id: ${{ secrets.AZURE_CLIENT_ID }}
                    tenant-id: ${{ secrets.AZURE_TENANT_ID }}
                    subscription-id: ${{ secrets.AZURE_SUBSCRIPTION_ID }}
    ```

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
             - uses: azure/login@v2
               with:
                creds: ${{ secrets.AZURE_CREDENTIALS }}
    ```
  
  ---

5. Use the Azure SQL Deploy action to connect to your SQL instance. You should have a dacpac package (`Database.dacpac`) at the root level of your repository. Use the `AZURE_SQL_CONNECTION_STRING` GitHub secret you created earlier.

    ```yaml
    - uses: azure/sql-action@v2.3
      with:
        connection-string: ${{ secrets.AZURE_SQL_CONNECTION_STRING }}
        path: './Database.dacpac'
        action: 'publish'
    ```

5. Complete your workflow by adding an action to logout of Azure. Here's the completed workflow. The file appears in the `.github/workflows` folder of your repository.

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
                 - uses: azure/login@v2
                   with:
                    client-id: ${{ secrets.AZURE_CLIENT_ID }}
                    tenant-id: ${{ secrets.AZURE_TENANT_ID }}
                    subscription-id: ${{ secrets.AZURE_SUBSCRIPTION_ID }}
             - uses: azure/sql-action@v2.3
               with:
                connection-string: ${{ secrets.AZURE_SQL_CONNECTION_STRING }}
                path: './Database.dacpac'
                action: 'publish'

                # Azure logout 
                 - name: logout
                   run: |
                     az logout
    ```

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
             - uses: azure/login@v2
               with:
                creds: ${{ secrets.AZURE_CREDENTIALS }}
             - uses: azure/sql-action@v2.3
               with:
                connection-string: ${{ secrets.AZURE_SQL_CONNECTION_STRING }}
                path: './Database.dacpac'
                action: 'publish'

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
