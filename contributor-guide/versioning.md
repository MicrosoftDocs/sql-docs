---
title: Repo sql-docs-pr and OPS Versioning, for Writers
description: Conceptual versioning guidance for writers in sql-docs-pr
ms.date: 10/29/26
---

# SQL versioning guidance and scenarios for writers

This article explains the effects of the OPS conceptual versioning system and the needs of the **MicrosoftDocs/sql-docs-pr** GitHub repo.

You must understand the versioning system in **sql-docs-pr** when documenting product applicability for the many supported platforms or versions of the [SQL Database Engine](https://learn.microsoft.com/sql/database-engine/sql-database-engine).

## SQL versioning quickstart

SQL Database Engine content uses versioning. Select a SQL Server service or product version from the left-hand TOC to scope the content that's displayed.

:::image type="content" source="media/versioning/dropdown.png" alt-text="A screenshot from the table of contents version dropdown list.":::

You can associate SQL services and product versions with monikers. The following table is crucial to document the versioning codes for moniker metadata and `monikerRange` syntax in articles. It contains the current monikers that you can use in SQL articles to control versioning in two different docsets:

| Area               | Moniker in sql-docs /docs/ docset | Moniker in /azure-sql/ docset   | Associated service or product version |
|--------------------|-------------------------------|--| ---------------------------------------|
| All                | `sqlallproducts-allversions`  |  | Everything. No filter.                |
| SQL Server         | `sql-server-ver17`            |  | SQL Server 2025                       |
|                    | `sql-server-ver16`            |  | SQL Server 2022                       |
|                    | `sql-server-ver15`            |  | SQL Server 2019                       |
|                    | `sql-server-2017`             |  | SQL Server 2017                       |
|                    | `sql-server-2016`             |  | SQL Server 2016                       |
|                    | `sql-server-previousversions` |  | SQL Server previous versions          |
| SQL Server (Linux) | `sql-server-linux-ver17`      |  | SQL Server 2025 for Linux             |
|                    | `sql-server-linux-ver16`      |  | SQL Server 2022 for Linux             |
|                    | `sql-server-linux-ver15`      |  | SQL Server 2019 for Linux             |
|                    | `sql-server-linux-2017`       |  | SQL Server 2017 for Linux             |
| Azure SQL          |                               | `azuresql` | All Azure SQL products       |
|                    | `azuresqldb-current`          | `azuresql-db` | Azure SQL Database         |
|                    | `azuresqldb-mi-current`       | `azuresql-mi` | Azure SQL Managed Instance |
|                    |                               | `azuresql-vm` | SQL Server on Azure VMs |
| SSAS               | `sql-analysis-services-2016`  |  | Analysis Services for SQL Server 2016 |
| SQL Server PDW (removed)    | `aps-pdw-2016`                |  | APS and PDW for SQL Server 2016-AU6   |
|                    | `aps-pdw-2016-au7`            |  | APS and PDW for SQL Server 2016-AU7   |
| Azure Synapse Analytics | `azure-sqldw-latest`     |  | Azure Synapse Analytics, formerly known as Azure SQL Data Warehouse. |
| Microsoft Fabric SQL analytics endpoint and Data Warehouse | `fabric` | | This includes both the SQL analytics endpoint and Data Warehouse in Microsoft Fabric. |
| SQL database in Fabric | `fabric-sqldb`            | `fabricsql` | This includes SQL database in Microsoft Fabric. |

Versioning happens at three levels:

- Folder
- Article
- Zone

### Folder-level defaults

Each folder in the repo has a versioning default assigned in the **sql-docs-pr/docs/docfx.json** file. As a result, new articles automatically get versioned based on their location.

Changes are often more complex than they appear at a surface level. Take great care when making changes.

### Article-level metadata

When the folder default versioning doesn't work for an article, add a **monikerRange** metadata value to the article. This metadata value overrides the folder-level defaults. Consider the following portion of metadata:

```yaml
monikerRange: ">= sql-server-linux-2017 || >= sql-server-2016 || = sqlallproducts-allversions"
---
```

In this example, the article applies to:

- Linux for SQL Server 2017 and greater: `>= sql-server-linux-2017`
- All versions of SQL Server 2016 and greater: `>= sql-server-2016`
- User selected All SQL: `= sqlallproducts-allversions`

### Zone-level versioning within articles

Inside an article, you can view version-specific **zones** of text. Consider the following examples:

```markdown
::: moniker range="=sql-server-2017"
|What's new  | Release notes  |
|---------|---------|
|[What's New in SQL Server 2017](../sql-server/what-s-new-in-sql-server-2017.md) |[SQL Server 2017 Release Notes](../sql-server/sql-server-2017-release-notes.md)|
::: moniker-end

::: moniker range="=sql-server-2016"
|What's new  | Release notes  |
|---------|---------|
|[What's New in SQL Server 2016](../sql-server/what-s-new-in-sql-server-2016.md)| [SQL Server 2016 Release Notes](../sql-server/sql-server-2016-release-notes.md)|
::: moniker-end
```

The examples use the same moniker range concept, but it applies to zones inside the article. A different table is displayed when it's scoped to SQL Server 2017 versus SQL Server 2016. Microsoft Learn customizes the user's experience.

> [!TIP]
> For a complete example, see the markdown for the main [SQL Server documentation](https://learn.microsoft.com/sql/sql-server/sql-server-technical-documentation) article. Experiment with the versioning selector on the left. Then select the **Edit** button on the right to view the markdown (just don't submit any edits).

## SQL versioning guidance

The following sections provide more detail about the concepts behind SQL versioning and scenarios involving versioning. All regular contributors should review and learn this information.

- In the `sql-docs-pr` repo, the `/docs/` docset contains the documentation content for supported versions of Microsoft SQL Server and all versions of the product via the SQL reference content. Before June 2018, the repo also contained the content for SQL Server 2014. 
    - Docs in this docset appear in `https://learn.microsoft.com/sql/`.
- In the `sql-docs-pr` repo, the `/azure-sql/` docset contains documentation specifically for Azure SQL Database, Azure SQL Managed Instance, SQL Server on Azure Arc, and SQL Server on Azure VMs, including the IaaS extension. This docset links extensively to the `/docs/` docset for SQL reference, as to all SQL Database Engine products.
    - Docs in this docset appear in `https://learn.microsoft.com/azure/azure-sql/`.

> [!NOTE]
> Versions 2005-2012 are available in the [SQL Server previous versions documentation](https://learn.microsoft.com/previous-versions/sql/). But these much older versions don't participate in versioning.

### SQL has complex versioning requirements

Most other documentation repos have simpler requirements for versioning than the `sql-docs-pr` repo. The reasons include:

- The family of SQL Server and the different closely related products is more diverse and nuanced than the families of other repos. SQL includes:
  - On-premises.
  - Two products in the Azure cloud, one being Azure SQL Database Managed Instance.
  - Offshoot products like Azure SQL data warehouse.
  - A Linux-based product.
  - Several tools that might or might not be a direct part of the primary SQL Server product.
  - Tools you can install like SSMS. They're designed so that customers never have any motivation to use an older version.
  
- Many articles apply only to SQL Server on-premises, and don't apply to Azure SQL Database. Care must be taken for product applicability, including addressing gaps of information presented as *context by omission*. Starting in 2026 we are expending extra effort to be explicit as to which products are NOT applicable. See the *fix context by omission* agent skill.

- In the `sql-docs-pr` repo, the high-level folders under **docs/** were never designed to group articles by their versioning attributes.

## Prerequisites

### Primer on versioning for reference content

To display different versions of content, you can implement versioning through *monikers*. In the URL, a moniker is the value of the `?view=` parameter. Here's an example:

```http
https://learn.microsoft.com/en-us/sql/database-engine/sql-database-engine?view=sql-server-ver17
```

This URL scopes the article to the `sql-server-ver17` version. 

Similarly, the URL of an API content page can include the `view=` parameter to show a specific version of the API.

```HTTP
https://learn.microsoft.com/dotnet/api/system.string?view=netframework-4.5
https://learn.microsoft.com/dotnet/api/system.string?view=netframework-4.7
```

A moniker isn't mandatory in the URL. The system smartly falls back to the proper default moniker. In a moniker family, the default is usually the moniker that has the largest value in the OPS version **Order** column (this control panel isn't public, contact the Cloud Knowledge Studio to make changes). The system avoids monikers that are marked as either `IsPrerelease=true` or `IsDeprecated=true`.

To determine the default moniker within the family, the system follows this sequence:

1. For monikers that aren't prerelease or deprecated, the default moniker is the one that has the largest value for **Order**.
1. If half of the monikers are prerelease and the other half are deprecated, the default is the prerelease moniker that has the largest value for **Order**.
1. If all the monikers are deprecated, the default is the deprecated moniker that has the largest **Order** value.

## docfx.json encodes the default monikers

The `fileMetadata` element in the real **docs/docfx.json** file applies the `monikerRange` by default at the folder level in this and other repos.

Avoid adding a **monikerRange** into an article's metadata when it would merely duplicate all the same moniker comparators that the article otherwise inherits from its high-level folder.

### Which moniker comparators can you use?

The only valid list of moniker comparators you can choose from exists in the `groups` element of the **docs/docfx.json** file. To add a new moniker to a docset, you must add it to this list.

For example,

```json
    "groups": {
      "gr-sql-2016-a": {
        "dest": "temp2016a",
        "moniker_range": "=azuresqldb-current || =azuresqldb-mi-current || =azure-sqldw-latest || >=aps-pdw-2016 || >=sql-server-2016 || >=sql-server-linux-2017 || =fabric || =fabric-sqldb"
      }
    },
```

## OPS portal for our packages

The versioning system recognizes and honors only the monikers that you tag as *Live* on the **Packages** display in the OPS portal. The monikers that are useful to the `sql-docs-pr` repo are part of the *Family* named **SQL Server family**. Outsiders shouldn't add or change monikers in **SQL Server family**.

## Editions are not in our versioning

For the `sql-docs-pr` repo, the monikers provide filtering at the level of *product* and *version*. Those two dimensions are complicated enough. It's unrealistic to reach for the more granular concept of *edition* which are the differences between the on-premises SQL Server editions.

## sqlallproducts-allversions is a special moniker

After the OPS Versioning implementation, each article webpage shows a drop-down box containing clickable lines of product and version combinations. When you select a different line, Microsoft Learn might instantly reduce the TOC to display only the subset of articles that have a moniker range that includes the chosen product and version line. The Versioning requirements for the `sql-docs-pr` repo are complex enough that customers can become frustrated by your inability to make the whole TOC reappear (it's a long explanation).

To mitigate this ongoing frustration of a much hidden TOC, the team added the special moniker *sqlallproducts-allversions* into the family of monikers. In the `sql-docs-pr` repo, always include the moniker comparator of **= sqlallproducts-allversions** in every moniker range, at all precedence levels. Most other repos have no similar moniker, but at least one other repo does.

> [!WARNING]
> All moniker ranges, at all precedence levels, must include the moniker comparator of **= sqlallproducts-allversions**!

## Live examples of precedence levels

### How to find which articles contain a YAML moniker range

#### Find by using a **cmd.exe** window

```console
cd sql-docs-pr
cd docs
findstr /s /l /i /n /c:"monikerRange" *.md
```

#### Find by using PowerShell

The following code example shows long syntax commented out, and then the equivalent short syntax.

```powershell
# Get-ChildItem -Recurse:$true -Include "*.md" | Select-String "monikerRange";
gci -r -i "*.md" | sls "monikerRange";
```

#### Find by using a **bash.exe** window

```bash
grep 'monikerRange' -E -r *
```

### How to find which articles contain a ::: zone

By using a **cmd.exe** window, you can find articles that contain a Versioning *zone*.

```console
findstr /s /l /i /n /c:":::" *.md
```

## Terminology of OPS versioning

- *Conceptual versioning:* Distinct from *Reference versioning*, which applies to autogenerated reference content.

- *Moniker in URL:* The unique ID or unique name for each package row in the OPS portal. A moniker is a combination of *product* and *version*, such as **sql-server-2017**. A moniker represents a *shippable unit*.
  - By itself, a moniker doesn't include any relational operator or conjoining operator.

- *Friendly name above TOC:* The moniker name as it appears in plain language, such as "Azure SQL Database".

- *Moniker comparator:* For example, **&gt;= sql-server-2016**. A moniker comparator is the combination of a relational operator and moniker. It's equivalent to saying a node in a moniker range. See *moniker range*.

- *Moniker range:* A string of one or more moniker comparators, conjoined by a conjoining operator.
  - For example, the moniker range of **&gt;= sql-server-2016 || = azuresqldb-current** has two *moniker comparators*. The two comparators are conjoined by the conjoining operator OR.

- *Operator:* Two kinds of operators are used with monikers to form ranges. Consider the example *moniker comparator* of **&gt;= sql-server-2016 || = azuresqldb-current**:
    - *Relational operator:* The **&gt;=**, meaning later than or equal to. Absence defaults to **=**.
  - *Conjoining operator:*  The **||**, meaning boolean OR. Absence defaults to AND. The explicit syntax **&&** for AND is invalid. AND is rarely used.

- *OPS:* Open Publishing System. Contact a Cloud Knowledge Studio Knowledge Manager to access OPS.

- *Precedence hierarchy:* An article's YAML `monikerRange` overrides its folder default in `docfx.json`; these metadata ranges aren't combined. Zone-level ranges filter sections within the article's effective range and don't add versions to the article.
  - *Folder-based:* The least granular level. It's specified in the **docfx.json** file.
    - Unless it's explicitly set up otherwise, the folder moniker range cascades down to all its child and other descendant folders in GitHub.
  - *Article YAML-based:* The metadata value **monikerRange** in the YAML at the top inside an article's MD file.
  - *Zone-based:* A **:::** delimited section within the body of an article. It's the most granular level.

