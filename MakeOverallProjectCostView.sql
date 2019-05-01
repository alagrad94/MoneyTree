SELECT p.ProjectName, p.StartDate, p.CompletionDate, p.AmountCharged, c.FirstName, c.LastName, c.PhoneNumber, c.Email, pc.DateUsed, pc.Quantity,
ci.ItemName, um.UnitName, cc.CategoryName, cpu.Cost, cpu.StartDate, cpu.EndDate
FROM Project p
LEFT JOIN Customer c ON p.CustomerId = c.Id
LEFT JOIN ProjectCost pc ON p.Id = pc.ProjectId
LEFT JOIN CostItem ci ON pc.CostItemId = ci.Id
LEFT JOIN CostPerUnit cpu ON pc.DateUsed BETWEEN cpu.StartDate AND cpu.EndDate
LEFT JOIN CostCategory cc ON ci.CostCategoryId = cc.Id
LEFT JOIN UnitOfMeasure um ON ci.UnitOfMeasureId = um.Id 