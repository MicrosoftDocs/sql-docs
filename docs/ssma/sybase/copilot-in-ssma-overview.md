---
title: What Is Microsoft Copilot in SSMA for SAP ASE?
description: Learn how Copilot in SQL Server Migration Assistant (SSMA) for SAP ASE provides intelligent, AI-powered assistance to convert complex or unsupported objects.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: niball, subasak
ms.date: 07/24/2026
ms.service: sql
ms.subservice: ssma
ms.topic: overview
ms.collection:
  - ce-skilling-ai-copilot
ms.update-cycle: 180-days
---
# What is Microsoft Copilot in SSMA for SAP ASE (SybaseToSQL)?

Copilot in SQL Server Migration Assistant (SSMA) enhances the code conversion process from SAP ASE to Transact-SQL by providing intelligent, AI-powered assistance for complex or unsupported objects. When the SSMA rule engine encounters conversion challenges, such as syntax differences, proprietary SAP ASE features, or unsupported data types, Copilot steps in to analyze the issue, explain the root cause, and generate alternative SQL-compatible code.

Copilot in SQL Server Migration Assistant (SSMA) is generally available from SSMA version 10.4.

By integrating with Azure OpenAI, Copilot in SSMA for SAP ASE allows you to review, refine, and validate the suggested code directly within the SSMA interface, streamlining the migration process and reducing manual effort.

## Prerequisites

To use Copilot in SSMA for SAP ASE for code conversion, you can use one of the following methods:

### Option 1: Azure OpenAI resource (Bring your own key)

- Azure OpenAI Endpoint URL.
- Azure OpenAI Deployment.
- Model name.
- Azure OpenAI key.

If you don't have these details, see the [Modify Azure OpenAI settings](#modify-azure-openai-settings) section.

### Option 2: Microsoft-managed endpoint with Microsoft Entra ID authentication 

SSMA for SAP ASE 10.6 makes this authentication type for Copilot in SSMA generally available.

- No manual key entry required.
- Sign in using your Microsoft Entra ID credentials.
- Authentication is handled through a browser-based authentication flow.

## Steps to run Copilot in SSMA for SAP ASE

After code conversion, the tool displays warnings or errors for target objects that the SSMA rule engine can't convert. For those objects, you can select **Fix with Copilot**.

If the OpenAI resource isn't registered, the tool prompts you with an authentication selection form. You can choose between:

- **Microsoft Entra ID Authentication** (new flow).
- **Azure OpenAI Key** (existing flow).

### Microsoft Entra ID authentication flow 

1. Select **Microsoft Entra ID Authentication**, and then select **Next**.
1. A browser window opens and prompts you to sign in.
1. After authentication, the Copilot interface loads, and you can review the suggested code fixes.

If you select the Copilot icon again, the authentication form doesn't reappear unless you sign out or reset the settings.

To sign out:

- Select the profile icon in the upper-right corner, and then select **Log out**.

Alternatively, go to **Tools** > **Project Settings** > **Copilot**, clear the saved credentials, and apply changes.

### Azure OpenAI key flow

If you want to bring your own key and the OpenAI resource isn't registered, complete the following fields:

- Azure OpenAI Endpoint URL.
- Azure OpenAI Deployment.
- Model name.
- Azure OpenAI key.

:::image type="content" source="media/copilot-in-ssma-overview/validate-endpoint.png" alt-text="Screenshot of Validate endpoint dialog.":::

After successful validation, you can view the converted code. It can take a few minutes to generate the converted code. After the code is generated, you can review the suggested changes.

## Code conversion interface

The Code Conversion window has three sections:

| Section | Description |
| --- | --- |
| **Errors to fix** | Shows the possible errors that the SSMA rule engine couldn't convert or failed to parse in the SQL statement. |
| **Explanation** | Provides the root cause of conversion issues, describes the logic behind the suggestions, and offers clear, contextual insights to help you understand and validate the generated Transact-SQL code. |
| **Code review window** | Allows you to view side-by-side differences between SSMA-generated and Copilot-converted code. It highlights changes so that you can evaluate improvements, understand modifications, and make informed decisions before accepting suggestions. |

:::image type="content" source="media/copilot-in-ssma-overview/compare-code.png" alt-text="Screenshot of code comparison dialog." lightbox="media/copilot-in-ssma-overview/compare-code.png":::

You can retry or send additional information in the prompt if you're not satisfied with the conversion. If the converted code is suitable, you can accept the suggestions.

## Manage accepted changes in the IDE

After you review and accept the Copilot-generated code, find the accepted changes in the SSMA IDE. Follow these steps to save and synchronize the changes with your database:

1. Go to the SSMA IDE where the accepted changes are displayed.
1. Save the changes to store them locally.
1. Use the synchronization feature to replicate the changes to your database.

## Modify Azure OpenAI settings

If you need to change the Azure OpenAI details, go to **Tools** > **Project Settings** > **Copilot** in the SSMA menu. Update the **Azure OpenAI Endpoint**, **Azure OpenAI Deployment**, **Model Name**, and **Azure OpenAI Key** as needed.

## Review and validation

Because AI generates this code, you must review, validate, and test the changes before you accept or save them. Ensure that the code meets your requirements and functions correctly in your environment.

## Limitations

You can't save Copilot-generated code for tables and user-defined data types directly in SSMA. In these cases, SSMA provides a download option to save the Copilot-generated code locally.

## Limits of managed endpoint

The Microsoft-managed endpoint for Copilot in SSMA has the following service limits:

| Limit | Value |
| --- | --- |
| Source script | Fewer than 16,000 tokens per request |
| User prompt | Fewer than 2,000 tokens per request |
| Customer request rate | 15 requests per minute |
| Customer request volume | 7,200 requests per 8-hour period |
| Aggregate service rate | 75 requests per minute across all users |

These limits apply when you use the Microsoft-managed endpoint with Microsoft Entra ID authentication.

## Related content

- [What's new in SSMA for SAP ASE](what-s-new-in-ssma-for-sybase-sybasetosql.md)
- [Microsoft Copilot in Azure with Azure SQL Database](/azure/azure-sql/copilot/copilot-azure-sql-overview)
