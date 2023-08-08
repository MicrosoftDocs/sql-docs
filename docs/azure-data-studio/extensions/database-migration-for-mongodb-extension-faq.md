---
title:  Azure Cosmos DB Migration for MongoDB extension FAQ
description: This article lists some frequently asked questions when using Azure Cosmos DB Migration for MongoDB extension.
author: sandnair
ms.author: sandnair
ms.reviewer: 
ms.date: 04/14/2023
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

## Frequently Asked Questions

The below section lists some commonly encountered issues, and their respective remediation actions.
### Question 1

**Question:** I'm unable to run assessment as my "Run Validation" is failing. How can I fix it? .

**Answer:** Refer to error displayed on the extension to see why the validation is failing. Typically the issue is inability to connect to the mongo endpoint or the user might not have sufficient privileges on the connected server to run the assessment.

**Remediation actions:** To run assessment, the user connected to MongoDb should have [readAnyDatabase](https://www.mongodb.com/docs/manual/reference/built-in-roles/#mongodb-authrole-readAnyDatabase) and [clusterMonitor](https://www.mongodb.com/docs/manual/reference/built-in-roles/#mongodb-authrole-clusterMonitor) roles assigned on the connected MongosDB cluster.  

Connect as user that either has above roles or use [grantRolesToUser](https://www.mongodb.com/docs/manual/reference/method/db.grantRolesToUser/) to configure appropriate roles for the current connected user.

### Question 2

**Question:**  I'm unable to see collection names and database names for assessments in "Feature Compatibility" category. How can I get that information?

**Answer:** The assessment uses "serverStatus" command to perform the feature compatibility assessment. Since this command doesn't provide the details of database or collection names, we are on unable to report them.

 **Remediation actions:** Rerun the assessment by providing the folder path containing the MongoDB profiler logs in the *Log Folder Path* field for more granular assessment details 
 
 ### Question 3
 
**Question:** How do I collect Log Messages? 

**Answer:** You can locate the log file at this path /var/log/mongodb/mongodb.log, if the log file is not found, please check the MongoDB config file. 

**Remediation actions:** To know more about logs, refer [here](https://www.mongodb.com/docs/manual/reference/log-messages/)
