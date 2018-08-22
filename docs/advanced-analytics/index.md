### YamlMime:YamlDocument
documentType: LandingData
title: SQL Server MLS and Extensibility Documentation
metadata:
  document_id: 
  title: SQL Server MLS Documentation - Tutorials, API Reference
  meta.description: Learn how to use embedded R, Python, Java with SQL Server relational data.
  ms.topic: landing-page
  ms.date: 08/27/2018
  ms.author: heidist
  author: HeidiSteen
  manager: cgronlun
  ms.prod: sql
  ms.technology: machine-learning
  services:
  ms.service: 
  ms.tgt_pltfrm: na
  ms.devlang:
abstract:
  description: SQL Server machine learning extends the database engine to include embedded R, Python, and Java for running external code on resident relational data.<br/><br/>R and Python libraries include base distributions, extended with Microsoft libraries that add predictive analytics at scale.<br/><br/>Java extensions are available in SQL Server vNext only as a preview feature. Operationalize Java code in stored procedures, or access through Transact-SQL on local relational data.
  aside:
    image:
      alt: 
      height: 110
      src: https://docs.microsoft.com/sql/advanced-analytics/media/index/ml-service-value-add.png
      width: 250  
    title: Execute R and Python on SQL Server. (3:00)
    href: https://www.youtube.com/watch?v=ACejZ9optCQ
    width: 250
sections:
- items:
  - type: list
    style: cards
    className: cardsM
    columns: 3
    items:
      - href: /sql/advanced-analytics/what-is-sql-server-machine-learning
        html: <p>Open-source R, extended with Microsoft AI algorithms for machine learning workloads and statistical analysis, visualization, and data manipulation at scale.</p>
        image:
          src: /media/index/placeholder.svg
        title: R
      - href: /sql/advanced-analytics/what-is-sql-server-machine-learning
        html: <p>Python developers can use Microsoft libraries for predictive analytics and machine learning at scale, using functions from revoscalepy and microsoftml. Anaconda and Python 3.5-compatible libraries are supported. </p>
        image:
          src: /media/index/logo_python.svg
        title: Python
      - href: /sql/advanced-analytics/what-is-sql-server-machine-learning
        html: <p>(SQL Server vNext only) Java developers can wrap code in stored procedures or in a binary format accessible through Transact-SQL.</p>
        image:
          src: /media/index/logo_java.svg
        title: Java
        
- title: 5-Minute Quickstarts
  items:
  - type: paragraph
    text: 'Embed analytics using familiar languages and high-performance libraries from Microsoft.'
  - type: list
    style: icon48
    items: 
    - image: 
        src: 
      text: R
      href: 
    - image: 
        src: 
      text: Python
      href: 
    - image: 
        src: 
      text: Java
      href:
- title: Step-by-Step Tutorials
  items:
  - type: paragraph
    text: Learn how to execute script in T-SQL or in stored procedures.
  - type: list
    style: ordered
    items:
    - html: <a href="/sql/advanced-analytics/tutorials/sql-dev-r-tutorials">How to execute R from T-SQL and stored procedures</a>
    - html: <a href="/sql/advanced-analytics/tutorials/sqldev-in-database-python-for-sql-developers">How to embed analytics in Python</a>
    - html: <a href="/sql/advanced-analytics/tutorials/rtsql-create-a-predictive-model">Create a predictive model in R</a>