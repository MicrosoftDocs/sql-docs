---
title: "Reporting Import Flat File failures | Microsoft Docs"
ms.custom: ""
ms.date: "04/13/2018"
ms.prod: "sql"
ms.prod_service: "database-engine, sql-database"
ms.service: ""
ms.component: "import-export"
ms.reviewer: "douglasl"
ms.suite: "sql"
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.swb.importflatfilesupport.f1"
author: "yualan"
ms.author: "alayu"
manager: "craigg"
ms.workload: "Inactive"
monikerRange: "= azuresqldb-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Reporting Import Flat File failures

The [Import Flat File Wizard](https://docs.microsoft.com/en-us/sql/relational-databases/import-export/import-flat-file-wizard/) in SSMS is powered by the [Microsoft PROSE SDK](https://microsoft.github.io/prose/), which is a framework for automatic programming or data wrangling from input-output examples. Although the Wizard can convert most Flat File sources, the PROSE team will consider updates to their algorithm based on any failures you experience.

## Send an email

If you run into an import failure or have a suggestion for feature improvement, send an email to sqltoolsprosesupport@microsoft.com

In this email:
- Include which version of SSMS you are running (**Help** -> **About**)
- Attach a snippet/sample of your flat file, say the first few lines including column names (**Do not include any Personal identifiable information (PII)**) 
- Brief description of the error you are running into and how you would like it to behave

Sending this sample helps improve the Import Flat File Wizard experience since this information is directly sent to the PROSE team. Thank you for your help.

## Learn More

Learn more about the wizard.

- **Learn more about the Import Flat File Wizard.** If you are looking for an overview of the Import Flat File wizard, see [Import Flat File Wizard](https://docs.microsoft.com/en-us/sql/relational-databases/import-export/import-flat-file-wizard/).
- **Learn more about PROSE.** If you are looking for an overview of the intelligent framework used by this wizard, see [PROSE SDK](https://microsoft.github.io/prose/).