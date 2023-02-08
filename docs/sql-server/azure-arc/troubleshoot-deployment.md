---
title: Troubleshoot Azure Arc-enabled SQL Server deployment  #Required; this page title is displayed in search results; Always include the word "troubleshoot" in this line.
description: Describes how to troubleshoot Azure Arc-enabled SQL Server deployment. #Required; this article description is displayed in search results.
author: MikeRayMSFT #Required; your GitHub user alias — correct capitalization is needed.
ms.author: mikeray #Required; Microsoft alias of the author.
ms.topic: troubleshooting-general #Required; leave this attribute/value as-is.
ms.date: 02/01/2023 #Required; enter the date in the mm/dd/yyyy format.
ms.prod: sql
---


<!-- Remove all the comments in this template before you sign-off or merge to the main branch.

This template provides the basic structure of a general troubleshooting article pattern. See the
[instructions troubleshooting -general](contribute-how-to-general-troubleshoot.md) in the pattern
library.

You can provide feedback about this template at: https://aka.ms/patterns-feedback

We write general troubleshooting articles when a specific error message isn't known.

-->

<!-- 1. H1 -----------------------------------------------------------------------------

Required: The headline (H1) is the primary heading at the top of the article. Pick an H1 that
clearly conveys what the content's about.

The heading of the general troubleshooting article should concisely describe the issue that the
customer is trying to fix. Make sure to include the word **troubleshoot** somewhere in the H1 of the
article to improve SEO.

-->

# Troubleshoot issues with Azure Arc-enabled SQL Server deployment

TODO: Add your heading

<!-- 2. Introductory paragraph ----------------------------------------------------------

Required: Lead with a light intro that describes, in customer-friendly language, what the customer
will do. Answer the fundamental “why would I want to do this?” question. Keep it short. Readers
should have a clear idea of what they will do in this article after reading the introduction.

-->

[Add your introductory paragraph]
TODO: Add your introductory paragraph

<!-- 3. Prerequisites --------------------------------------------------------------------

Optional: If there are prerequisites for the task covered by the how-to guide, make
**Prerequisites** your first H2 in the guide. The prerequisites H2 is never numbered. Use clear and
unambiguous language and use a unordered list format. If there are specific versions of software a
user needs, call out those versions (for example: Visual Studio 2019 or later). It's OK to link to
content to assist them before they begin.

-->

## Prerequisites
TODO: Determine if prerequisites are appropriate
TODO: List the prerequisites if appropriate

<!-- 4. Potential quick workarounds --------------------------------------------------------------------

Optional: An issue might be able to be temporarily resolved with a quick fix. If known, list any
workarounds that can be implemented quickly to resolve the issue. Link to information about
longer-term solutions in the Solution section.

-->

## Potential quick workarounds
TODO: Add your workarounds

### Workaround 1: \<summarize the key info in the workaround>

1. Step 1.
2. Step 2.

### Workaround 2: \<summarize the key info in the workaround>

1. Step 1.
2. Step 2.

<!-- 5. Troubleshooting check list --------------------------------------------------------------------

Required: Provide the guidance/instruction about how the customer can troubleshoot the issues and
determine the cause of the issue.

-->

## Troubleshooting check list
TODO: Add a troubleshooting checklist using the steps below

### Step 1

### Step 2

### Step 3

<!-- 6. Cause/solution --------------------------------------------------------------------

Required: Provide a descriptive H2 for each cause. H2 is helpful for SEO and the right-side
navigation. To identify the issue and how to prevent it from happening again, the cause of the issue
should be defined if known.

Make sure that the H3 solution headings clearly state the intention of the Solution section. Each
Solution section should have a short sentence that describes the steps that are about to be taken.

-->

## Cause 1: \<summarize the key info of the cause>
TODO: Add a description of the cause.

### Solution 1: \<summarize the key info of the solution>
TODO: Add the steps for the solution

1. Step 1.
2. Step 2.

### Solution 2: \<summarize the key info of the solution>

1. Step 1.
2. Step 2.

## Cause 2: \<summarize the key info of the cause>
TODO: Add a description of the cause.

### Solution 1: \<summarize the key info of the solution>

1. Step 1.
2. Step 2.

### Solution 2: \<summarize the key info of the solution>

1. Step 1.
2. Step 2.

<!--- 7. Advanced troubleshooting and data collection ----------------------------------------------

Optional: Include this section if advanced troubleshooting steps are needed and may require a call
to support. List any information or procedures in this section to help the customer submit a support
ticket.

-->

## Advanced troubleshooting and data collection

TODO: List any information or procedures in this section to help the customer prepare for submitting a support ticket.

<!--- 8. Next steps ----------------------------------------------

Optional: List any next steps that should be taken after the issue has been initially resolved.

-->

## Next steps
TODO: Add your next step link(s)

- Next step 1
- Next step 2

<!--- 9. Reference ----------------------------------------------

Optional: -->

## Reference
TODO: Add your reference link(s)

- Reference 1
- Reference 2


## Troubleshoot Azure extension for SQL Server
Before you start, note the logs location. The extension log is created in this folder:
`C:\ProgramData\GuestConfig\extension_logs\Microsoft.AzureData.WindowsAgent.SqlServer\ExtensionLog_0.log`

The deployer logs are created in this folder:
`C:\ProgramData\GuestConfig\extension_logs\Microsoft.AzureData.WindowsAgent.SqlServer\1.1.0.0\deployer.log`

The failure to create the Arc-enabled SQL Server resource could be caused by several issues.

### Extension installation failed

Go to the connected server and check the deployer log. You should see the below messages.

`[07/14/2021 18:56:45 UTC] [INFO]          Status of service 'SqlServerExtension' before attempting start: Stopped`
`[07/14/2021 18:56:45 UTC] [INFO]          Status of service 'SqlServerExtension' after attempting start: Stopped`

If you can't see it means the extension didn't install properly. Try the following steps.

1. Check event logs to see if anything preventing installation. Try installing SqlServerExtension.msi from the following folder `C:\Packages\Plugins\Microsoft.AzureData.WindowsAgent.SqlServer{version}`. The installation UI may provide the error details.

2. Close services app, server manager and retry one of the [connection methods](./connect.md) to install the extension, and see if that helps.

### Extension installed but didn't start

Check the log files for any application errors.

### Server - Azure Arc ARM resource was manually deleted

Check the extension log for the following record:

```output
[7/14/2021 9:36:18 PM UTC] [ERROR]   [UploadServiceProvider]      [ExtensionHandlerStatusQueryError] ArcSqlInstancesRequest request is null, not sending data to RP.
```

This means the machine is no longer recognized as a connected server. [Onboard the server to Azure Arc](/azure/azure-arc/servers/onboard-portal) and retry one of the [connection methods](./connect.md) to install the extension.

### Server managed identity has insufficient permissions

Check the extension log for the following record:
`[INFO] [UploadServiceProvider] [ExtensionHandlerArcUploadServicesNotifications] [AzureUpload] Arc post request failed with error: Forbidden message: {"ErrorDescription":{"ErrorCode":6,"Message":"The user has no access to the provided Azure resource."},"ResponseUrl":null}`

Make sure the machine's managed identity has been assigned the *Azure Connected SQL Server Onboarding* role. See [When machine already connected to Arc-enabled Server](connect-already-enabled.md) role assignment instructions.

### The user didn't migrate the Arc-enabled SQL Server resource to the new resource provider

Check the extension log for the following record:
`[7/14/2021 5:35:04 PM UTC] [INFO] [UploadServiceProvider] [ExtensionHandlerArcUploadServicesNotifications] [AzureUpload] Arc for Sql Server upload response status: InternalServerError.`

Make sure to migrate the Arc-enabled SQL Server resource to `Microsoft.AzureArcData` following [these steps](.\release-notes.md#breaking-change-1).