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

### Logs

The extension stores errors, warnings, and other diagnostic logs in the default log directory:

- Windows - `C:\Users\<username>.dmamongo\logs\`
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
 
**Question:** How do I collect profiler logs and what are the implications of enabling profiler? 

**Answer:** Profiling may have an effect on MongoDB performance, especially when configured with a profiling level of 2, or when using a low threshold value is used with a profiling level of 1. Profiling also consumes another disk space, so you would need to make sure you have enough space available. You would need to carefully consider any performance and other implications while enabling profiler. You should preferably enable it in a nonproduction environment or during off-peak hours. 

**Remediation actions:** To collect profile logs, follow the steps [here](https://www.mongodb.com/docs/manual/tutorial/manage-the-database-profiler/)

 ### Question 4
 
**Question:** How long should the profiler be left on?

**Answer:** The duration for which you leave the profiler on depends upon your workload. You should leave it on long enough to capture all the regular operation patterns within your workload in the log file. Only the entries captured by the logs are assessed by the extension. The judgment regarding the right duration for your workload (for example, 2 hrs, 2 days or more) would be yours to take, based upon the known access patterns and their frequency. 
