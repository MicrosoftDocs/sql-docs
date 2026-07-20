---
title: Versioning System SQL Docs
description: "Versioning system for SQL documentation"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 09/08/2025
ms.service: sql
ms.subservice: release-landing
ms.topic: concept-article
monikerRange: "=azuresqldb-current || =azuresqldb-mi-current || =azure-sqldw-latest || >=aps-pdw-2016 || >=sql-server-linux-2017 || >=sql-server-2017"
---

# Versioning system for SQL documentation

[!INCLUDE [sql-asdb-asa-pdw](../includes/applies-to-version/sql-asdb-asa-pdw.md)]

This article explains our *versioning system* for SQL documentation. The versioning system knows about products and their versions. The system enables you to choose the product and version you're interested in. The system then displays the appropriate documentation.

## *Applies to* products

Most of the SQL Server articles have the words **Applies to** under their title. On the same line, there follows a handy listing of SQL *products* with indicators of whether the article is relevant to the product. For example, the product SQL Server could be indicated as relevant, while Azure SQL Database could be indicated as irrelevant to the article.

The **Applies to** line doesn't know about *versions* of products. We strive to avoid discrepancies between the **Applies to** line and the products aspect of our versioning system configurations.

## History of separate file sets

For SQL Server 2014 and earlier versions, each version has its own full separate copy of the documentation files. For instance, the documentation for SQL Server 2014 began as a copy of the documentation for SQL Server 2012. The 2014 copy was then edited during the product development cycle.

This old approach meant that if a flaw was discovered in the 2014 documentation, the flaw might also exist in 2012 and 2008. This made fixing flaws and general maintenance more difficult.

## Multiple versions in the same files

For this reason and others, the documentation files for SQL Server 2016 are also for 2017, 2019, and probably for \<vNext\>. This consolidation is made practical because we now assign *versioning monikers* to our SQL Server documentation files. The versioning monikers are assigned, or are explicitly embedded, at whatever degree of granularity makes sense for each given documentation file.

## Versioning control in the UI

When you view any SQL documentation article by using our :::no-loc text="Docs"::: website, the currently chosen versioning moniker is visible above the table of contents (TOC). The control is a dropdown list.

![media_versioning-control-10-sql-server-2017.png](media/versioning-control-10-sql-server-2017.png)

If you want to see the documentation for a different version of SQL Server, you select the expander arrow located at the end of the current version moniker. Then select to choose any product and version combination you want. When you select a different version, the displayed documentation suddenly changes to show the differences for the newly chosen version. There might or might not be any changes, and both cases are common.

![media_versioning-control-20-expanded.png](media/versioning-control-20-expanded.png)

### HTTPS parameter :::no-loc text="view=":::

Each article whose web address begins with `https://learn.microsoft.com/sql/` has a parameter named `?view=` appended to its address. This parameter value is the versioning moniker code.

The moniker *code* in the `https` address always matches the moniker *name* that is displayed in the versioning control.

## Products, not editions

### Editions

In the 1990s and into the 2000s, Microsoft SQL Server had only one product. There were various *editions* of each version of SQL Server, such as the *Developer* and *Enterprise* editions of SQL Server 2008. The editions represented slightly different feature sets, but the core product was the same. New SQL Server releases might still have a variety of editions.

### Products

With the more recent rise of cloud computing and Microsoft Azure, Microsoft released its Azure SQL Database product. While there's much code shared by both the traditional SQL Server on-premises product and the Azure SQL Database product, these products are two truly separate products.

For SQL, versioning monikers make distinctions between products, but not between editions.

#### Azure cloud SQL products

For articles whole web addresses start with `https://learn.microsoft.com/sql/`, almost all apply to at least one version of the product named SQL Server. A large subset of those articles also apply to one or more of our SQL service products that are hosted in our Azure cloud. One such SQL cloud product is named Azure SQL Database.

Naturally, the Azure SQL Database product has only one version. Almost all the articles that apply to Azure SQL Database, but not to SQL Server, have web addresses starting with `https://learn.microsoft.com/azure/sql-database/`.

## Scenarios of version filtering

The versioning system works by filtering out all documentation content that doesn't apply to the currently active moniker. Each time you choose a different versioning moniker, set of content that is hidden changes. The filtering hides content at the following levels:

- Sections or sentences within an article.
- Entries for articles in the table of contents (TOC).

Next are scenarios that explain the effects of choosing a different moniker.

### Scenario 1: Within the current article

The following scenario focuses on sections within your current article:

1. The current versioning moniker is **SQL Server 2017**.
1. You're reading a section that describes a feature that was first added to version 2017 of SQL Server.
1. You change the moniker to **SQL Server 2016**.
1. You notice the section you were reading is gone.
1. You again change the moniker, this time to **SQL Server 2019**.
1. You notice the 2017 section you were reading is back on display.

In the preceding scenario, the section about the new 2017 feature is likely marked with a *moniker range* that includes the following moniker code:

- `>=sql-server-2017`

When the moniker **SQL Server 2019** was chosen, the versioning system realized that 2019 is greater-than-or-equal-to 2017, and it displayed the section.

### Scenario 2: Select a link to a hidden article

The following uncommon scenario explains what happens if you select a link to an article that is currently hidden from the table of contents (TOC). In brief, the link works:

1. The current versioning moniker is **SQL Server 2017**.
1. In the current article :::no-loc text="A":::, you select a link to an article :::no-loc text="B"::: that applies only to SQL Server 2016.
   - Before the selection, the table of contents has its entry for article :::no-loc text="B"::: hidden.
1. After the selection, article :::no-loc text="B"::: is displayed.
   - The display of article :::no-loc text="B"::: forces the versioning control to switch to the **SQL Server 2016** moniker.
   - Because the original moniker **SQL Server 2017** had to be abandoned. This abandoning causes an informational message to be displayed near the top of the webpage. The [message](#anchor-message-unavailable-for-moniker) explains that the current moniker had to be switched to accommodate the new article :::no-loc text="B":::.

### Scenario 3: Navigate to an https address

The following article was added new for SQL Server 2017. The article describes features that were added to SQL Server in version 2017. Most or all of those new features are also part of version 2019. Here are the attributes of the article.

| Attribute | Value |
| --- | --- |
| Title | What's new in SQL Server 2017 |
| moniker range | `=">= sql-server-2017"` |
| `https` address | `https://learn.microsoft.com/sql/sql-server/what-s-new-in-sql-server-2017` |

Given the base `https` address, the following table explains what happens when the `?view=` parameter is appended by the user, and with various values.

| Value of `?view=` | Behavior of the `https` address navigation |
| --- | --- |
| *(No parameter)* | The versioning system would try its default moniker value. Usually we set this to the latest non-Preview version of SQL Server.<br /><br />A default of SQL Server 2017 or 2019 would satisfy the attribute `>= sql-server-2017`.<br /><br />The system would append the parameter to the `https` address, perhaps as `?view=sql-server-2017`.<br />The versioning dropdown list control would then be set to the match moniker name. |
| `sql-server-2016` | The versioning system would realize that the moniker range of the article doesn't include version 2016.<br /><br />The system would then choose one of the monikers that does satisfy the range.<br /><br />Then as in the version 2016 case, the parameter `?view=` would be appended, and the control name would match the parameter value. |
| `sql-server-2017` | The versioning system understands that the parameter value is included in the moniker range of the article.<br /><br />The versioning control would be set to match the parameter value. |
| `sql-server-2019` | Same as for the case of value `sql-server-2017`, except the parameter and control are set to 2019. |

<a id="anchor-allsql-hidenothing"></a>

### All SQL - Hide nothing, special moniker

There's one special moniker product name of **All SQL**, and its only version is **Hide nothing**. The purpose of this moniker is for internal testing of certain changes. If used by a customer, this moniker is more likely to mislead than to inform.

Some articles have information pertaining to multiple versions of SQL Server. Every regular moniker hides versioned sections that could otherwise display information that is inaccurate, confusing, or contradictory for the moniker's version. The special **All SQL** moniker would display all the version sections, and it might not be obvious that inaccurate information is being displayed.

<a id="anchor-message-unavailable-for-moniker"></a>

## Message: The requested page isn't available for \<moniker\>

The following scenario leads to the display of an informational message near the top of the :::no-loc text="Docs"::: webpage:

1. Currently the versioning moniker is **SQL Server 2017**.
1. You're reading an article that is relevant to SQL Server 2017.
   - The article is *not* relevant to the product Azure SQL Database.
1. You attempt to change the moniker to **Azure SQL Database - current**.
1. You see your attempt was rejected, and a message is displayed.

At the end of this scenario, you see the following informational message displayed near the top of the Docs webpage:

> The requested page isn't available for Azure SQL Database - current. You have been redirected to the newest product version this page is available for.

The *newest* version might exclude versions that aren't yet fully released and are in *Preview* status.

![media_versioning-control-30-viewfallbackfrom.png](media/versioning-control-30-viewfallbackfrom.png)

## Previous versions of SQL Server

The versioning system is fully implemented for SQL Server version 2016 onward.

- *2012 and earlier:* &nbsp; The versioning system isn't used for SQL Server 2012 or earlier versions.
  - The special moniker of **SQL Server - older** is meant to hide almost all articles. The rare exceptions are a couple of articles that customers of older versions might need once.
  - [Previous versions of SQL Server documentation](previous-versions-sql-server.md)

- *2014:* &nbsp; The versioning system is half implemented for SQL Server 2014. You can choose SQL Server 2014 in the versioning control, and it works. Yet internally the files for 2014 are dedicated to just 2014, the same way the files for 2008 are dedicated to just 2008.
  - [Install SQL Server documentation to view offline in SSMS](sql-server-offline-documentation.md)

- *2016 and later:* &nbsp; The versioning system is fully implemented for SQL Server 2016 and later versions.
  - [Welcome to SQL Server documentation 2016 and later](./index.yml?preserve-view=true&view=sql-server-2016)
  - [Install SQL Server documentation to view offline in SSMS](sql-server-offline-documentation.md)

## Related content

- [Previous versions of SQL Server documentation](previous-versions-sql-server.md)
- [SQL Server docs navigation guide](sql-docs-navigation-guide.md)
- [How to contribute to SQL Server documentation](sql-server-docs-contribute.md)
