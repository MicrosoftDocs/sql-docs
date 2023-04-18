---
title: Connect to and query Azure SQL Database using Python and the `pyodbc` library
description: Learn how to connect to a database in Azure SQL Database and query data using Python
author: bobtabor-msft
ms.author: rotabor
ms.custom: passwordless-python, ai-gen-docs
ms.date: 04/18/2023
ms.service: sql-database
ms.subservice: security
ms.topic: quickstart
monikerRange: "= azuresql || = azuresql-db"
---

# Connect to and query Azure SQL Database using Python and the `pyodbc` library

This quickstart describes how to connect an application to a database in Azure SQL Database and perform queries using Python and the `pyodbc` library. This quickstart follows the recommended passwordless approach to connect to the database. You can learn more about passwordless connections on the passwordless hub.

> [!NOTE]
> This article was partially created with the help of artificial intelligence. Before publishing, an author reviewed and revised the content as needed. See [Our principles for using AI-generated content in Microsoft Learn](https://aka.ms/ai-content-principles).

## Prerequisites

* An [Azure subscription](https://azure.microsoft.com/free/dotnet/).
* An Azure SQL database configured with Azure Active Directory (Azure AD) authentication. You can create one using the [Create database quickstart](./single-database-create-quickstart.md).
* The latest version of the [Azure CLI](/cli/azure/get-started-with-azure-cli).
* Visual Studio Code with the [Python extension](https://marketplace.visualstudio.com/items?itemName=ms-python.python).
* Python 3.6 or later.

## Configure the database

Secure, passwordless connections to Azure SQL Database with Python require certain database configurations. Verify the following settings on your logical server in Azure to properly connect to Azure SQL Database in both local and hosted environments:

For local development connections, make sure your logical server has a firewall rule enabled to allow your client IP address to connect:

1. Navigate to the Networking page of your server.

1. Toggle the Selected networks radio button to show additional configuration options.

1. Select Add your client IPv4 address(xx.xx.xx.xx) to add a firewall rule that will enable connections from your local machine IPv4 address. Alternatively, you can also select + Add a firewall rule to enter a specific IP address of your choice.

The server must also have Azure AD authentication enabled with an Azure Active Directory admin account assigned. For local development connections, the Azure Active Directory admin account should be an account you can also log into Visual Studio Code or the Azure CLI with locally. You can verify whether your server has Azure AD authentication enabled on the Azure Active Directory page.

If you're using a personal Azure account, make sure you have Azure Active Directory setup and configured for Azure SQL Database in order to assign your account as a server admin. If you're using a corporate account, Azure Active Directory will most likely already be configured for you.

## Create the project

For the steps ahead, create a new Python project using Visual Studio Code.

1. Open Visual Studio Code and create a new folder for your project.

1. Create a new Python file called `app.py`.

## Install the `pyodbc` library

To connect to Azure SQL Database using Python, install the `pyodbc` library. This package acts as a data provider for connecting to databases, executing commands, and retrieving results.

In the terminal, run the following command to install the `pyodbc` package:

```bash
pip install pyodbc
```

## Add code to connect to Azure SQL Database

The `pyodbc` library uses a class called `DefaultAzureCredential` to implement passwordless connections to Azure SQL Database, which you can learn more about on the DefaultAzureCredential overview. DefaultAzureCredential is provided by the Azure Identity library on which the SQL client library depends.

Complete the following steps to connect to Azure SQL Database using the `pyodbc` library and `DefaultAzureCredential`:

1. Update the environment variables in your local system to include the passwordless connection string. Remember to update the `<your database-server-name>` and `<your-database-name>` placeholders.

The passwordless connection string includes a configuration value of `Authentication=Active Directory Default`, which instructs the app to use `DefaultAzureCredential` to connect to Azure services. This functionality is implemented internally by the `pyodbc` library. When the app runs locally, it authenticates with the user you're signed into Visual Studio Code with. Once the app deploys to Azure, the same code discovers and applies the managed identity that is associated with the hosted app, which you'll configure later.

1. Add the following sample code to the `app.py` file. This code performs the following important steps:

* Retrieves the passwordless connection string from the environment variables
* Creates a `Person` table in the database during startup (for testing scenarios only)
* Creates a function to retrieve the `Person` records stored in the database
* Creates a function to add new `Person` records to the database

```python
import os
import pyodbc

connection_string = os.environ["AZURE_SQL_CONNECTIONSTRING"]

try:
    # Table would be created ahead of time in production
    conn = pyodbc.connect(connection_string)
    cursor = conn.cursor()

    cursor.execute("""
        CREATE TABLE Persons (
            ID int NOT NULL PRIMARY KEY IDENTITY,
            FirstName varchar(255),
            LastName varchar(255)
        );
    """)

    conn.commit()
except Exception as e:
    # Table may already exist
    print(e)

def get_persons():
    rows = []

    with pyodbc.connect(connection_string) as conn:
        cursor = conn.cursor()
        cursor.execute("SELECT * FROM Persons")

        for row in cursor.fetchall():
            rows.append(f"{row.ID}, {row.FirstName}, {row.LastName}")

    return rows

def create_person(first_name, last_name):
    with pyodbc.connect(connection_string) as conn:
        cursor = conn.cursor()
        cursor.execute(f"INSERT INTO Persons (FirstName, LastName) VALUES (?, ?)", first_name, last_name)
        conn.commit()
```

## Run and test the app locally

The app is ready to be tested locally. Make sure you're signed into Visual Studio Code or the Azure CLI with the same account you set as the admin for your database.

1. Run the `app.py` file in Visual Studio Code.

1. Use a tool like Postman or curl to test the API endpoints for creating and retrieving `Person` records.

## Deploy to Azure App Service

The app is ready to be deployed to Azure. Follow the official documentation on how to deploy a Python app to Azure App Service using Visual Studio Code.

## Connect App Service to Azure SQL Database

Follow the instructions in the original article to connect the App Service instance to Azure SQL Database using Service Connector or Azure portal.

## Test the deployed application

Browse to the URL of the app to test that the connection to Azure SQL Database is working. You can locate the URL of your app on the App Service overview page. Append the `/person` path to the end of the URL to browse to the same endpoint you tested locally.

The person you created locally should display in the browser. Congratulations! Your application is now connected to Azure SQL Database in both local and hosted environments.