public static void executeUpdateStatement(Connection con) {
   try {
      String SQL = "INSERT INTO TestTable (Col2, Col3) VALUES ('a', 10)";
      Statement stmt = con.createStatement();
      int count = stmt.executeUpdate(SQL);
      System.out.println("ROWS AFFECTED: " + count);
      stmt.close();
   }
   catch (Exception e) {
      e.printStackTrace();
   }
}