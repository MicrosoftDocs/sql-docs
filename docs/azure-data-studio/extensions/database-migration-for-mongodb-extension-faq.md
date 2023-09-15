---
title: Azure Cosmos DB Migration for MongoDB extension FAQ
description: This article lists some frequently asked questions when using Azure Cosmos DB Migration for MongoDB extension.
author: sandnair
ms.author: sandnair
ms.reviewer: randolphwest
ms.date: 08/21/2023
ms.service: azure-data-studio
ms.topic: conceptual
---
# Azure Cosmos DB Migration for MongoDB extension FAQ

This article lists some frequently asked questions when using Azure Cosmos DB Migration for MongoDB extension.

## Logs

The extension stores errors, warnings, and other diagnostic logs in the default log directory:

- Windows - `C:\Users\<username>\.dmamongo\logs\`
- Linux - `~/.dmamongo/logs`
- macOS - `/Users/<username>/.dmamongo/logs`

## Frequently asked questions

The below section lists some commonly encountered issues, and their respective remediation actions.

### How do I run my assessment if "Run Validation" is failing?

**Answer:** Refer to error displayed on the extension to see why the validation is failing. Typically the issue is inability to connect to the mongo endpoint or the user might not have sufficient privileges on the connected server to run the assessment.

**Remediation actions:** To run assessment, the user connected to MongoDb should have [readAnyDatabase](https://www.mongodb.com/docs/manual/reference/built-in-roles/#mongodb-authrole-readAnyDatabase) and [clusterMonitor](https://www.mongodb.com/docs/manual/reference/built-in-roles/#mongodb-authrole-clusterMonitor) roles assigned on the connected MongosDB cluster.

Connect as user that either has above roles or use [grantRolesToUser](https://www.mongodb.com/docs/manual/reference/method/db.grantRolesToUser/) to configure appropriate roles for the current connected user.

### How do I see collection names and database names for assessments in the "Feature Compatibility" category?

**Answer:** The assessment uses "serverStatus" command to perform the feature compatibility assessment. Since this command doesn't provide the details of database or collection names, we are on unable to report them.

**Remediation actions:** Rerun the assessment by providing the folder path containing the MongoDB profiler logs in the *Log Folder Path* field for more granular assessment details.

### How do I collect log messages?

**Answer:** You can locate the log file at the following path: `/var/log/mongodb/mongodb.log`. If the log file isn't found, check the location in the MongoDB config file.

**Remediation actions:** For more information, see [Log Messages](https://www.mongodb.com/docs/manual/reference/log-messages/).
