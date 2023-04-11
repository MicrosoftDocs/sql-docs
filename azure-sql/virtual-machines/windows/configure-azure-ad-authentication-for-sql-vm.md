---
title: "Enable Azure AD authentication"
description: This article teaches you to configure Azure AD authentication for your SQL Server on Azure VM.
author: adbadram
ms.author: adbadram
ms.reviewer: mathoma
ms.date: 02/10/2023
ms.service: virtual-machines-sql
ms.subservice: security
ms.topic: how-to
---
# Enable Azure AD authentication for SQL Server on Azure VMs
[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article teaches you to enable Azure Active Directory (Azure AD) authentication for your SQL Server on Azure Virtual Machine. 

## Overview

Starting with SQL Server 2022, you can connect to SQL Server on Azure VM using one of the following Azure AD identity authentication methods: 

- Azure AD Password
- Azure AD Integrated
- Azure AD Universal with Multi-Factor Authentication
- Azure Active Directory access token 


When enabling a [managed identity](/azure/active-directory/managed-identities-azure-resources/overview#managed-identity-types) for SQL Server on Azure Virtual Machines, the security boundary of the identity is the resource to which it's attached. For example, the security boundary for a virtual machine with managed identities for Azure resources enabled is the virtual machine. Any code running on that VM is able to call the managed identities endpoint and request tokens. The experience is similar experience when working with other resources that support managed identities. For more information, read the [Managed Identities FAQ](/azure/active-directory/managed-identities-azure-resources/managed-identities-faq).

## Prerequisites

To enable Azure AD authentication to  your SQL Server, you need the following prerequisites: 

- Use SQL Server 2022. 
- Register SQL Server VM with the [SQL Server Iaas Agent extension](sql-agent-extension-manually-register-single-vm.md). 
- Have an existing **system-assigned** or **user-assigned** managed identity in the same Azure AD tenant as your SQL Server VM. 

## Grant permission to the identity

The identity you choose to authenticate to SQL Server has to have either the **Azure AD Directory Readers role** permission or the following three Microsoft Graph application permissions (app roles): `User.ReadALL`, `GroupMember.Read.All`, and `Application.Read.All`. 

The steps in this section teach you how to add your managed identity to the **Azure AD Directory Readers role**. You need to have Azure AD Global administrator privileges to make changes to the Directory Readers role assignments. If you do not have sufficient permission, work with your Azure AD administrator to follow the steps in the section and grant **Azure AD Directory Readers** role permissions to the managed identity you want to use to authenticate to your SQL Server on your Azure VM. 


To grant your managed identity the **Azure AD Directory** role permission, follow these steps: 

1. Go to **Azure Active Directory** in the [Azure portal](https://portal.azure.com). 
1. On the **Azure Active Directory** overview page, choose **Roles and administrators** under **Manage**: 

      :::image type="content" source="media/configure-azure-ad-authentication-for-sql-vm/azure-ad-overview-portal.png" alt-text="Screenshot of the Azure AD overview page in the Azure portal, with Roles and administrators selected.":::

1. Type _Directory readers_ in the search box, and then select the role **Directory readers** to open the **Directory Readers | Assignments** page: 

   :::image type="content" source="media/configure-azure-ad-authentication-for-sql-vm/search-for-directory-readers.png" alt-text="Screenshot of the Roles and administrators page of the Azure portal, searching for and selecting the Directory Readers role.":::

1. On the **Directory Readers | Assignments** page, select **+ Add assignments** to open the **Add assignment** page. 

   :::image type="content" source="media/configure-azure-ad-authentication-for-sql-vm/azure-ad-directory-readers.png" alt-text="Screenshot of the Directory Readers page of the Azure portal.":::

1. On the **Add assignments** page, choose **No member selected** under **Select members** to open the **Select a member** page. 

   :::image type="content" source="media/configure-azure-ad-authentication-for-sql-vm/azure-ad-add-assignment.png" alt-text="Screenshot of the add assignment page of the Azure portal, with No member selected highlighted.":::

1. On the **Select a member** page, search for the managed identity you want to use with your SQL Server VM and add to the **Azure AD Directory Readers** role. If you want to use a system-assigned managed identity, search for the name of the VM and select the associated identity. If you want to use a user-managed identity, then search for the name of the identity and choose it. Select **Select** to save your identity selection and go back to the **Add assignments** page. 

   :::image type="content" source="media/configure-azure-ad-authentication-for-sql-vm/azure-ad-select-member.png" alt-text="Screenshot searching for members to select in the Azure portal.":::

1. Verify that you see your chosen identity under **Select members** and then select **Next**.  

   :::image type="content" source="media/configure-azure-ad-authentication-for-sql-vm/azure-ad-verify-assignment.png" alt-text="Screenshot of the Add assignment page in the Azure portal, with VM2 added as an assignment.":::

1. Verify that your assignment type is set to **Active** and the box next to **Permanently assigned** is checked. Enter a business justification, such as _Adding Directory Reader role permissions to the system-assigned identity for VM2_ and then select **Assign** to save your settings and go back to the **Directory Readers | Assignments** page. 

   :::image type="content" source="media/configure-azure-ad-authentication-for-sql-vm/azure-ad-verify-assignment-settings.png" alt-text="Screenshot of settings on the Add assignment in the Azure portal.":::

1. On the **Directory Readers | Assignments** page, confirm you see your newly added identity under **Directory Readers**. 

   :::image type="content" source="media/configure-azure-ad-authentication-for-sql-vm/azure-ad-verify-directory-reader.png" alt-text="Screenshot of the Directory Readers page of the Azure portal showing your VM assignment added to the role.":::


## Enable Azure AD authentication to your SQL VM

To enable Azure AD authentication to your SQL Server VM, follow these steps: 

1. Navigate to your [SQL virtual machines resource](manage-sql-vm-portal.md#security-configuration) in the Azure portal. 
1. Select **Security configuration** under **Settings**. 
1. Choose **Enable** under **Azure AD authentication**. 
1. Choose the managed identity type from the drop-down, either **System-assigned** or **User-assigned**. If you choose user-assigned, then select the identity you want to use to authenticate to SQL Server on your Azure VM from the **User-assigned managed identity** drop-down that appears. 

   :::image type="content" source="media/configure-azure-ad-authentication-for-sql-vm/enable-azure-ad-in-portal.png" alt-text="Screenshot of the security configuration page for SQL VM in the Azure portal, with Azure AD authentication selected.":::

> [!NOTE]
> The error `The selected managed identity does not have enough permissions for Azure AD Authentication` indicates that permissions have not been properly assigned to the identity you've selected. Check the [Grant permissions](#grant-permission-to-the-identity) section to assign proper permissions. 


## Limitations

Consider the following limitations: 

- The identity you choose to authenticate to SQL Server has to have either the **Azure AD Directory Readers** role permissions or the following three Microsoft Graph application permissions (app roles): `User.ReadALL`, `GroupMember.Read.All`, and `Application.Read.All`. 
- Once Azure AD authentication is enabled, there is no way to disable it by using the Azure portal. 
- Currently, enabling Azure AD authentication is only possible through the Azure portal. 
- Currently, Azure AD authentication is only available to SQL Server VMs deployed to the public cloud. 
- Azure AD authentication is only supported for Azure Windows VMs. 
- Currently, authenticating to SQL VM through Azure AD authentication using [FIDO2 method](/azure/active-directory/authentication/howto-authentication-passwordless-faqs) is not supported. 

## Next steps

Review the security best practices for [SQL Server](/sql/relational-databases/security/). 

For other topics related to running SQL Server in Azure VMs, see [SQL Server on Azure Virtual Machines overview](sql-server-on-azure-vm-iaas-what-is-overview.md). If you have questions about SQL Server virtual machines, see the [Frequently asked questions](frequently-asked-questions-faq.yml).

To learn more, see the other articles in this best practices series:

- [Quick checklist](performance-guidelines-best-practices-checklist.md)
- [VM size](performance-guidelines-best-practices-vm-size.md)
- [Storage](performance-guidelines-best-practices-storage.md)
- [HADR settings](hadr-cluster-best-practices.md)
- [Collect baseline](performance-guidelines-best-practices-collect-baseline.md)
