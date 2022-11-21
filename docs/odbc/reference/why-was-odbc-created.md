---
description: "Why Was ODBC Created?"
title: "Why Was ODBC Created? | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC [ODBC], about ODBC"
ms.assetid: ba6eb993-316b-4650-bab8-d76583c00e53
author: David-Engel
ms.author: v-davidengel
---
# Why Was ODBC Created?
Historically, companies used a single DBMS. All database access was done either through the front end of that system or through applications written to work exclusively with that system. However, as the use of computers grew and more computer hardware and software became available, companies started to acquire different DBMSs. The reasons were many: People bought what was cheapest, what was fastest, what they already knew, what was latest on the market, what worked best for a single application. Other reasons were reorganizations and mergers, where departments that previously had a single DBMS now had several.  
  
 The issue grew even more complex with the advent of personal computers. These computers brought in a host of tools for querying, analyzing, and displaying data, along with a number of inexpensive, easy-to-use databases. From then on, a single corporation often had data scattered across a myriad of desktops, servers, and minicomputers, stored in a variety of incompatible databases, and accessed by a vast number of different tools, few of which could get at all of the data.  
  
 The final challenge came with the advent of client/server computing, which seeks to make the most efficient use of computer resources. Inexpensive personal computers (the clients) sit on the desktop and provide both a graphical front end to the data and a number of inexpensive tools, such as spreadsheets, charting programs, and report builders. Minicomputers and mainframe computers (the servers) host the DBMSs, where they can use their computing power and central location to provide quick, coordinated data access. How then was the front-end software to be connected to the back-end databases?  
  
 A similar problem faced independent software vendors (ISVs). Vendors writing database software for minicomputers and mainframes were usually forced to write one version of an application for each DBMS or write DBMS-specific code for each DBMS they wanted to access. Vendors writing software for personal computers had to write data access routines for each different DBMS with which they wanted to work. This often meant a huge amount of resources were spent writing and maintaining data access routines rather than applications, and applications were often sold not on their quality but on whether they could access data in a given DBMS.  
  
 What both sets of developers needed was a way to access data in different DBMSs. The mainframe and minicomputer group needed a way to merge data from different DBMSs in a single application, while the personal computer group needed this ability as well as a way to write a single application that was independent of any one DBMS. In short, both groups needed an interoperable way to access data; they needed open database connectivity.
