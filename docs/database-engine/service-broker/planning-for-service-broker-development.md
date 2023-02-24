---
title: Planning for Service Broker Development
description: "Review the following as you design a Service Broker application."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Planning for Service Broker Development

Review the following as you design a Service Broker application:

- The metrics concerning the type and volume of input and output expected from your application.

- The requirements for your proposed application.

If you understand these factors, you can develop a system that meets business goals.

## Planning Checklist

Consider the following questions as you plan your application:

- **What role does Service Broker play in your application?**

    The answer to this question helps you plan the message types your application uses, the structure of your application, and the storage and processing needs of your application.

    For example, your application could use Service Broker to deal with spikes in message arrival rates by storing the messages in queues until resources are available to process them. In this case, the message types your application uses should closely match the input and output of the existing application. You can estimate storage and processing needs for your application, depending on the existing workload.

    In contrast, if you design a new application, carefully consider which operations can benefit the most from Service Broker. Using Service Broker often trades off predictable processing times in the best case in return for reliability when a conventional application would fail completely.

    For example, an online order entry application might not have to completely process the order and provide final confirmation at the time the order is submitted. Instead, the application might submit the order to a service that processes the order and provides final confirmation through e-mail. By using this design, the order application can continue to accept orders even if networking problems prevent the application from confirming the order. When the networking problems are resolved, the application processes the orders. In this case, the storage and processing needs for the application depend on the number of orders expected, the size of each order message, and the time each order requires to be processed.

- **What information will be required in a conversation in order to complete the desired task? What are the schemas of the messages that the endpoints will exchange to provide this information to each other?**

    Most services exchange semi-structured information. Therefore, XML encoding a good choice. A binary encoding is useful for exchanging binary files such as images. When a message communicates only the fact that the message arrived, use an empty message.

    By selecting the correct message type, you are less likely to have to update your application later. Depending on the message type encoding, updates can include anything from having to update an XML schema file to having to make significant coding changes in your application. If there are data items that you do not currently need but you expect to need in the future, it might make sense to include them. If you define these elements in the schema to begin with, you will not have to change the schema when you do support them.

- **Where will your message processing logic run?**

    You can design your application as a stored procedure that is activated by Service Broker, as a background service, as a scheduled event, or as an external application. The final decision depends on the role that Service Broker plays in your application. For example, if your application processes a continuous stream of messages that arrive at a predictable rate, you might use a background service. If your application must scale dynamically based on the number of messages arriving, you can use a stored procedure activated by a queue. If your application holds messages in a queue and processes all the messages at a set time, you can use a scheduled event to start the application.

    You can use an external application if your program requires access to resources outside the database, such as Web pages or files. Using an external application can improve the scalability of your application, because processing occurs on the mid-tier servers instead of on the database server. It is easy to scale out an application that uses Service Broker, because Service Broker provides remote transactional access to queues. Any application that can send Transact-SQL commands to the database and process the results can use Service Broker.

    Each external program is isolated from other programs that use the queue. Therefore, the external programs do not need special precautions to manage access to the queue. Additionally, if the connection fails while the application is processing a message, the transaction rolls back and Service Broker returns the message to the queue. Network problems cannot cause the application to lose a message.

- **What technology do you plan to use to implement your application?**

    You can implement an external application by using any technology that can connect to the database and run Transact-SQL statements in SQL Server. However, applications are typically developed in a .NET Framework-compatible language and ADO.NET. You can implement a stored procedure in either Transact-SQL or one of the .NET Framework-compatible languages. Transact-SQL can provide better performance against the Database Engine. The CLR-compliant languages can provide better flexibility, tighter control of program flow, better performance for processor-intensive applications, and direct access to the .NET Framework.

- **What server components will your application use most heavily?**

    Work with your system administrator to ensure that you have sufficient resources to achieve optimum application performance. Know which components you will use most frequently. For example, if your application uses queues to even out the processing workload, or turns on message retention, ensure that there is sufficient disk space for the queue to grow. Conversely, an application that has high messaging volumes but lower queue wait times might use more network bandwidth, but consume less disk space.

- **Will your messages have different priorities?**

    In heavily loaded systems, Service Broker conversation priorities help ensure that important work is not blocked by large amounts of less important work. Conversation priorities also enable designs that support different levels of service.

## See also

[Service Broker Programming Concepts](service-broker-programming-concepts.md)

[Developer Responsibilities for Service Broker](developer-responsibilities-for-service-broker.md)

[Service Architecture](service-architecture.md)
