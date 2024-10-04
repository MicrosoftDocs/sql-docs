---
title: "Create an SSIS migration assessment with the Data Migration Assistant"
description: Learn how to use Data Migration Assistant to assess an on-premises SQL Server Integration Service (SSIS) before migrating to Azure SQL Database or Azure SQL Managed Instance
author: chugugrace
ms.author: chugu
ms.reviewer: randolphwest
ms.date: 06/28/2024
ms.service: sql
ms.subservice: dma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
helpviewer_keywords:
  - "Data Migration Assistant, Assess"
---

# Perform a SQL Server Integration Service migration assessment with Data Migration Assistant

[!INCLUDE [deprecation-notice](includes/deprecation-notice.md)]

## Prerequisites

To assess SQL Server Integration Service(SSIS) packages, below components need to be installed with Data Migration Assistant:

- SQL Server Integration Service with the same version as the SSIS packages to assess.
- Azure Feature Pack or other third party components if SSIS packages to assess have these components.

DMA needs to run with **administrator** access to assess SSIS packages in Package Store.

> [!NOTE]  
> Source to SQL Server version 2019 and above aren't supported.

## Performance assessments

The following step-by-step instructions help you perform your first assessment for migrating SQL Server Integration Service (SSIS) packages to Azure SQL Database or Azure SQL Managed Instance, by using Data Migration Assistant.

[!INCLUDE [online-offline](../includes/azure-migrate-to-assess-sql-data-estate.md)]

## Create an assessment

1. Select the **New** (`+`) icon, and then select the **Assessment** project type as **Integration Service**.

1. Set the source and target server type.

   Select the source as **SQL Server**, and set the target server type as **Azure SQL Database** or **Azure SQL Managed Instance**.

1. Select **Create**.

   :::image type="content" source="media/dma-assess-ssis/dma-assess-ssis-create.png" alt-text="Screenshot of create assessment." lightbox="media/dma-assess-ssis/dma-assess-ssis-create.png":::

## Connect to a server

1. Follow the default option, and navigate to **Select sources**.
1. Enter the SQL Server instance name, choose the Authentication type, set the correct connection properties.
1. (Optional) Enter a folder path that contains SSIS packages.
1. (Optional) Enter package encryption password if applicable.
1. Select **Connect** to the source SQL Server.

   :::image type="content" source="media/dma-assess-ssis/dma-assess-ssis-addsource.png" alt-text="Screenshot showing the Connect to a server pane with the Enter a folder path that contains SSIS packages option and Enter package encryption password if applicable option called out." lightbox="media/dma-assess-ssis/dma-assess-ssis-addsource.png":::

## Add sources to assess

1. Select the SSIS package storage types to assess, and then select **Add**.

   :::image type="content" source="media/dma-assess-ssis/dma-assess-ssis-addsource-type.png" alt-text="Screenshot showing the Add sources pane." lightbox="media/dma-assess-ssis/dma-assess-ssis-addsource-type.png":::

1. Select **Add Sources** to open the connection flyout menu, if need assess multiple folders.

1. Select **Start Assessment**.

   :::image type="content" source="media/dma-assess-ssis/dma-assess-ssis-assess.png" alt-text="Screenshot of Start assessment." lightbox="media/dma-assess-ssis/dma-assess-ssis-assess.png":::

## View results

The Compatibility issues category provides partially supported or unsupported features that block migrating on-premises SSIS packages to Azure-SSIS Integration Runtime. It then provides recommendations to help you address those issues.

:::image type="content" source="media/dma-assess-ssis/dma-assess-ssis-result.png" alt-text="Screenshot of View results." lightbox="media/dma-assess-ssis/dma-assess-ssis-result.png":::

## Related content

- [Migrate on-premises SSIS workloads to SSIS in ADF or Synapse Pipelines](/azure/data-factory/scenario-ssis-migration-overview)
- [Migrate SQL Server Integration Services packages to an Azure SQL Managed Instance](/azure/dms/how-to-migrate-ssis-packages-managed-instance)
- [Redeploy SSIS packages to Azure SQL Database with Azure Database Migration Service](/azure/dms/how-to-migrate-ssis-packages)
