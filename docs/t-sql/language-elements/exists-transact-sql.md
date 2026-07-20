---
title: "EXISTS (Transact-SQL)"
description: EXISTS specifies a subquery to test for the existence of rows.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/05/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "EXISTS_TSQL"
  - "EXISTS"
helpviewer_keywords:
  - "existence testing [SQL Server]"
  - "testing existence"
  - "EXISTS keyword"
  - "subqueries [SQL Server], EXISTS keyword"
  - "queries [SQL Server], comparing"
  - "comparing queries"
  - "NOT EXISTS keyword"
  - "row existence testing [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# EXISTS (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Specifies a subquery to test for the existence of rows.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
EXISTS ( subquery )
```

## Arguments

#### *subquery*

A restricted `SELECT` statement. The `INTO` keyword isn't allowed. For more information, see the information about subqueries in [SELECT](../queries/select-transact-sql.md).

## Return types

**Boolean**

## Result values

Returns `TRUE` if a subquery contains any rows.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Use `NULL` in a subquery to still return a result set

The following example returns a result set with `NULL` specified in the subquery and still evaluates to `TRUE` by using `EXISTS`.

```sql
SELECT DepartmentID, Name
FROM HumanResources.Department
WHERE EXISTS (SELECT NULL)
ORDER BY Name ASC;
```

### B. Compare queries by using EXISTS and IN

The following example compares two queries that are semantically equivalent. The first query uses `EXISTS` and the second query uses `IN`.

```sql
SELECT a.FirstName,
       a.LastName
FROM Person.Person AS a
WHERE EXISTS (SELECT *
      FROM HumanResources.Employee AS b
      WHERE a.BusinessEntityID = b.BusinessEntityID
            AND a.LastName = 'Johnson');
GO
```

The following query uses `IN`.

```sql
SELECT a.FirstName,
       a.LastName
FROM Person.Person AS a
WHERE a.LastName IN (SELECT a.LastName
      FROM HumanResources.Employee AS b
      WHERE a.BusinessEntityID = b.BusinessEntityID
            AND a.LastName = 'Johnson');
GO
```

Here's the result set for either query.

```output
FirstName                                          LastName
-------------------------------------------------- ----------
Barry                                              Johnson
David                                              Johnson
Willis                                             Johnson
 ```

### C. Compare queries by using EXISTS and = ANY

The following example shows two queries to find stores whose name is the same name as a vendor. The first query uses `EXISTS` and the second uses `= ANY`.

```sql
SELECT DISTINCT s.Name
FROM Sales.Store AS s
WHERE EXISTS (SELECT *
      FROM Purchasing.Vendor AS v
      WHERE s.Name = v.Name);
GO
```

The following query uses `= ANY`.

```sql
SELECT DISTINCT s.Name
FROM Sales.Store AS s
WHERE s.Name = ANY (SELECT v.Name
      FROM Purchasing.Vendor AS v);
GO
```

### D. Compare queries by using EXISTS and IN

The following example shows queries to find employees of departments that start with `P`.

```sql
SELECT p.FirstName,
       p.LastName,
       e.JobTitle
FROM Person.Person AS p
     INNER JOIN HumanResources.Employee AS e
         ON e.BusinessEntityID = p.BusinessEntityID
WHERE EXISTS (SELECT *
      FROM HumanResources.Department AS d
            INNER JOIN HumanResources.EmployeeDepartmentHistory AS edh
               ON d.DepartmentID = edh.DepartmentID
      WHERE e.BusinessEntityID = edh.BusinessEntityID
            AND d.Name LIKE 'P%');
GO
```

The following query uses `IN`.

```sql
SELECT p.FirstName,
       p.LastName,
       e.JobTitle
FROM Person.Person AS p
     INNER JOIN HumanResources.Employee AS e
         ON e.BusinessEntityID = p.BusinessEntityID
     INNER JOIN HumanResources.EmployeeDepartmentHistory AS edh
         ON e.BusinessEntityID = edh.BusinessEntityID
WHERE edh.DepartmentID IN (SELECT DepartmentID
      FROM HumanResources.Department
      WHERE Name LIKE 'P%');
GO
```

### E. Use NOT EXISTS

`NOT EXISTS` works the opposite of `EXISTS`. The `WHERE` clause in `NOT EXISTS` is satisfied if no rows are returned by the subquery. The following example finds employees who aren't in departments which have names that start with `P`.

```sql
SELECT p.FirstName,
       p.LastName,
       e.JobTitle
FROM Person.Person AS p
     INNER JOIN HumanResources.Employee AS e
         ON e.BusinessEntityID = p.BusinessEntityID
WHERE NOT EXISTS (SELECT *
      FROM HumanResources.Department AS d
            INNER JOIN HumanResources.EmployeeDepartmentHistory AS edh
               ON d.DepartmentID = edh.DepartmentID
      WHERE e.BusinessEntityID = edh.BusinessEntityID
            AND d.Name LIKE 'P%')
ORDER BY LastName, FirstName;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
FirstName                      LastName                       Title
------------------------------ ------------------------------ ------------
Syed                           Abbas                          Pacific Sales Manager
Hazem                          Abolrous                       Quality Assurance Manager
Humberto                       Acevedo                        Application Specialist
Pilar                          Ackerman                       Shipping & Receiving Superviso
François                       Ajenstat                       Database Administrator
Amy                            Alberts                        European Sales Manager
Sean                           Alexander                      Quality Assurance Technician
Pamela                         Ansman-Wolfe                   Sales Representative
Zainal                         Arifin                         Document Control Manager
David                          Barber                         Assistant to CFO
Paula                          Barreto de Mattos              Human Resources Manager
Shai                           Bassli                         Facilities Manager
Wanida                         Benshoof                       Marketing Assistant
Karen                          Berg                           Application Specialist
Karen                          Berge                          Document Control Assistant
Andreas                        Berglund                       Quality Assurance Technician
Matthias                       Berndt                         Shipping & Receiving Clerk
Jo                             Berry                          Janitor
Jimmy                          Bischoff                       Stocker
Michael                        Blythe                         Sales Representative
David                          Bradley                        Marketing Manager
Kevin                          Brown                          Marketing Assistant
David                          Campbell                       Sales Representative
Jason                          Carlson                        Information Services Manager
Fernando                       Caro                           Sales Representative
Sean                           Chai                           Document Control Assistant
Sootha                         Charncherngkha                 Quality Assurance Technician
Hao                            Chen                           HR Administrative Assistant
Kevin                          Chrisulis                      Network Administrator
Pat                            Coleman                        Janitor
Stephanie                      Conroy                         Network Manager
Debra                          Core                           Application Specialist
Ovidiu                         Crãcium                        Sr. Tool Designer
Grant                          Culbertson                     HR Administrative Assistant
Mary                           Dempsey                        Marketing Assistant
Thierry                        D'Hers                         Tool Designer
Terri                          Duffy                          VP Engineering
Susan                          Eaton                          Stocker
Terry                          Eminhizer                      Marketing Specialist
Gail                           Erickson                       Design Engineer
Janice                         Galvin                         Tool Designer
Mary                           Gibson                         Marketing Specialist
Jossef                         Goldberg                       Design Engineer
Sariya                         Harnpadoungsataya              Marketing Specialist
Mark                           Harrington                     Quality Assurance Technician
Magnus                         Hedlund                        Facilities Assistant
Shu                            Ito                            Sales Representative
Stephen                        Jiang                          North American Sales Manager
Willis                         Johnson                        Recruiter
Brannon                        Jones                          Finance Manager
Tengiz                         Kharatishvili                  Control Specialist
Christian                      Kleinerman                     Maintenance Supervisor
Vamsi                          Kuppa                          Shipping & Receiving Clerk
David                          Liu                            Accounts Manager
Vidur                          Luthra                         Recruiter
Stuart                         Macrae                         Janitor
Diane                          Margheim                       Research & Development Enginee
Mindy                          Martin                         Benefits Specialist
Gigi                           Matthew                        Research & Development Enginee
Tete                           Mensa-Annan                    Sales Representative
Ramesh                         Meyyappan                      Application Specialist
Dylan                          Miller                         Research & Development Manager
Linda                          Mitchell                       Sales Representative
Barbara                        Moreland                       Accountant
Laura                          Norman                         Chief Financial Officer
Chris                          Norred                         Control Specialist
Jae                            Pak                            Sales Representative
Wanda                          Parks                          Janitor
Deborah                        Poe                            Accounts Receivable Specialist
Kim                            Ralls                          Stocker
Tsvi                           Reiter                         Sales Representative
Sharon                         Salavaria                      Design Engineer
Ken                            Sanchez                        Chief Executive Officer
José                           Saraiva                        Sales Representative
Mike                           Seamans                        Accountant
Ashvini                        Sharma                         Network Administrator
Janet                          Sheperdigian                   Accounts Payable Specialist
Candy                          Spoon                          Accounts Receivable Specialist
Michael                        Sullivan                       Sr. Design Engineer
Dragan                         Tomic                          Accounts Payable Specialist
Lynn                           Tsoflias                       Sales Representative
Rachel                         Valdez                         Sales Representative
Garrett                        Vargar                         Sales Representative
Ranjit                         Varkey Chudukatil              Sales Representative
Bryan                          Walton                         Accounts Receivable Specialist
Jian Shuo                      Wang                           Engineering Manager
Brian                          Welcker                        VP Sales
Jill                           Williams                       Marketing Specialist
Dan                            Wilson                         Database Administrator
John                           Wood                           Marketing Specialist
Peng                           Wu                             Quality Assurance Supervisor
```

## Examples: Azure Synapse Analytics and Analytics Platform System (PDW)

### F. Use EXISTS

The following example identifies whether any rows in the `ProspectiveBuyer` table could be matches to rows in the `DimCustomer` table. The query returns rows only when both the `LastName` and `BirthDate` values in the two tables match.

```sql
SELECT a.LastName, a.BirthDate
FROM DimCustomer AS a
WHERE EXISTS (SELECT *
      FROM dbo.ProspectiveBuyer AS b
      WHERE (a.LastName = b.LastName)
            AND (a.BirthDate = b.BirthDate));
```

### G. Use NOT EXISTS

`NOT EXISTS` works as the opposite as `EXISTS`. The `WHERE` clause in `NOT EXISTS` is satisfied if no rows are returned by the subquery. The following example finds rows in the `DimCustomer` table where the `LastName` and `BirthDate` don't match any entries in the `ProspectiveBuyers` table.

```sql
SELECT a.LastName,
       a.BirthDate
FROM DimCustomer AS a
WHERE NOT EXISTS (SELECT *
      FROM dbo.ProspectiveBuyer AS b
      WHERE (a.LastName = b.LastName)
            AND (a.BirthDate = b.BirthDate));
```

## Related content

- [Expressions (Transact-SQL)](expressions-transact-sql.md)
- [What are the SQL database functions?](../functions/functions.md)
- [WHERE (Transact-SQL)](../queries/where-transact-sql.md)
