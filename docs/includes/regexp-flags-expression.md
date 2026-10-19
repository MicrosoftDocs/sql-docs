---
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 05/05/2025
ms.service: sql
ms.topic: include
ms.custom:
  - build-2025
---

One or more characters that specify the modifiers used for searching for matches. Type is **varchar** or **char**, with a maximum of 30 characters.

For example, `ims`. The default is `c`. If an empty string `(' ')` is provided, it will be treated as the default value `('c')`. Supply `c` or any other character expressions. If flag contains multiple contradictory characters, then SQL Server uses the last character.

For example, if you specify `ic` the regex returns case-sensitive matching.

If the value contains a character other than those listed at [Supported flag values](#supported-flag-values), the query returns an error like the following example:

```output
Invalid flag provided. '<invalid character>' are not valid flags. Only {c,i,s,m} flags are valid.
```

##### Supported flag values

| Flag | Description |
| --- | --- |
| `i` | Case-insensitive (default `false`) |
| `m` | Multi-line mode: `^` and `$` match begin/end line in addition to begin/end text (default `false`) |
| `s` | Let `.` match `\n` (default `false`) |
| `c` | Case-sensitive (default `true`) |
