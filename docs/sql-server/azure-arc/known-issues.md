---
title: "Known issues"
description: "Identifies known issues for SQL Server enabled by Azure Arc"
author: MikeRayMSFT
ms.author: mikeray
ms.topic: troubleshooting-known-issue 
ms.date: 12/18/2023
#customer intent: As a database administrator, I want current issues related to SQL Server enabled by Azure Arc so that the organization can maximize the value of SQL Server with Azure Arc.
---

# Known issues: SQL Server enabled by Azure Arc

This article identifies current known issues with SQL Server enabled by Azure Arc

## Deleted database renders on portal

<!-- Required: Database inventory deletion - H2

Each known issue should be in its own section. 
Provide a title for the section so that users can 
easily identify the issue that they are experiencing.

-->

<!-- Required: Issue description (no heading) -->

Databases deleted on-premises might not be immediately deleted on Azure. There's no impact on how database CRUD operations happen on-premises but propagation to database inventory is being worked on. The fix is expected to be out soon.

<!--

## Prerequisites

<!--Optional: Prerequisites - H2

If this section is needed, make "Prerequisites" your
first H2 in the article.

Use clear and unambiguous language, and use
an unordered list format. 


### Troubleshooting steps

<!-- Optional: Troubleshooting steps - H3

Not all known issues can be corrected, but if a solution 
is known, describe the steps to take to correct the issue.

### Possible causes

#### Cause [number]

<!-- Optional: Possible causes - H3

In an H3 section, describe potential causes in one
or more H4 sections.

-->

## Related content

[Troubleshoot Azure extension for SQL Server](troubleshoot-deployment.md)

[Troubleshoot connectivity to the data processing service and telemetry endpoints](troubleshoot-telemetry-endpoint.md)

[Troubleshoot best practices assessment on SQL Server](troubleshoot-assessment.md)
