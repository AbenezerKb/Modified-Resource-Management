using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class resourceTablesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllocatedBudgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    projectId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    activity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<double>(type: "float", nullable: false),
                    contingency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    preparedBy = table.Column<int>(type: "int", nullable: false),
                    ApprovedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocatedBudgets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AllocatedResources",
                columns: table => new
                {
                    allocatedResourcesNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    projId = table.Column<int>(type: "int", nullable: false),
                    itemId = table.Column<int>(type: "int", nullable: false),
                    unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocatedResources", x => x.allocatedResourcesNo);
                });

            migrationBuilder.CreateTable(
                name: "AssignedWorkForces",
                columns: table => new
                {
                    assigneWorkForceNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    projId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedWorkForces", x => x.assigneWorkForceNo);
                });

            migrationBuilder.CreateTable(
                name: "BIDs",
                columns: table => new
                {
                    BIDID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    initailDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    finalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConBID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstimatedBID = table.Column<double>(type: "float", nullable: false),
                    ActualCost = table.Column<double>(type: "float", nullable: false),
                    PenalityDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fileName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BIDs", x => x.BIDID);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    clientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contractorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estimatedDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estimatedCost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateOfContract = table.Column<DateTime>(type: "datetime2", nullable: false),
                    attachmentOfContract = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.clientId);
                });

            migrationBuilder.CreateTable(
                name: "Consultants",
                columns: table => new
                {
                    consultantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    projectId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contractorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    changesTaken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reasonForChange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    defectsSeen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nextWork = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    attachemnt = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultants", x => x.consultantId);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ConId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConGiver = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConReciever = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    Attachement = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ConId);
                });

            migrationBuilder.CreateTable(
                name: "DailyLabors",
                columns: table => new
                {
                    LaborerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    projectId = table.Column<int>(type: "int", nullable: false),
                    wagePerhour = table.Column<double>(type: "float", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyLabors", x => x.LaborerID);
                });

            migrationBuilder.CreateTable(
                name: "Granders",
                columns: table => new
                {
                    GranderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectManager = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalEstiamtedReqtBudget = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Granders", x => x.GranderId);
                });

            migrationBuilder.CreateTable(
                name: "Incidents",
                columns: table => new
                {
                    incidentNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    incidentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    proID = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidents", x => x.incidentNo);
                    table.ForeignKey(
                        name: "FK_Incidents_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LaborDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dateOfWork = table.Column<DateTime>(type: "datetime2", nullable: false),
                    weekNo = table.Column<int>(type: "int", nullable: false),
                    dateType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    morningSession = table.Column<bool>(type: "bit", nullable: false),
                    afternoonSession = table.Column<bool>(type: "bit", nullable: false),
                    eveningSession = table.Column<bool>(type: "bit", nullable: false),
                    NoOfHrsPerSession = table.Column<int>(type: "int", nullable: false),
                    PaymentDayIn = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaborDetails", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SubContractors",
                columns: table => new
                {
                    SubId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubWorkId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubContractors", x => x.SubId);
                });

            migrationBuilder.CreateTable(
                name: "SubContractWorks",
                columns: table => new
                {
                    subconractingid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    workName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubContractWorks", x => x.subconractingid);
                });

            migrationBuilder.CreateTable(
                name: "TimeCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dateOfWork = table.Column<DateTime>(type: "datetime2", nullable: false),
                    employeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jobType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    weekNo = table.Column<int>(type: "int", nullable: false),
                    NoOfPresents = table.Column<int>(type: "int", nullable: false),
                    NoOfAbscents = table.Column<int>(type: "int", nullable: false),
                    NoOfHrsPerSession = table.Column<int>(type: "int", nullable: false),
                    totalWorkedHrs = table.Column<int>(type: "int", nullable: false),
                    wages = table.Column<double>(type: "float", nullable: false),
                    totalPayment = table.Column<double>(type: "float", nullable: false),
                    preparedByFK = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    approvedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LaborerID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeeklyRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    projId = table.Column<int>(type: "int", nullable: false),
                    projManager = table.Column<int>(type: "int", nullable: false),
                    projCoordinator = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    weekNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    specialRequest = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyRequirements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkForces",
                columns: table => new
                {
                    assigneWorkForceNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WokrkForceID = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    AssignedWorkForceassigneWorkForceNo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkForces", x => x.assigneWorkForceNo);
                    table.ForeignKey(
                        name: "FK_WorkForces_AssignedWorkForces_AssignedWorkForceassigneWorkForceNo",
                        column: x => x.AssignedWorkForceassigneWorkForceNo,
                        principalTable: "AssignedWorkForces",
                        principalColumn: "assigneWorkForceNo");
                });

            migrationBuilder.CreateTable(
                name: "ApprovedWorkLists",
                columns: table => new
                {
                    ApprovedWorkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsultantId = table.Column<int>(type: "int", nullable: false),
                    ApprovedWorks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovedWorkLists", x => x.ApprovedWorkId);
                    table.ForeignKey(
                        name: "FK_ApprovedWorkLists_Consultants_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "Consultants",
                        principalColumn: "consultantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeclinedWorkLists",
                columns: table => new
                {
                    DeclinedWorkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsultantId = table.Column<int>(type: "int", nullable: false),
                    DeclinedWorks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclinedWorkLists", x => x.DeclinedWorkId);
                    table.ForeignKey(
                        name: "FK_DeclinedWorkLists_Consultants_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "Consultants",
                        principalColumn: "consultantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DefectsCorrectionlists",
                columns: table => new
                {
                    DefectsCorrectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsultantId = table.Column<int>(type: "int", nullable: false),
                    DefectsCorrections = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefectsCorrectionlists", x => x.DefectsCorrectionId);
                    table.ForeignKey(
                        name: "FK_DefectsCorrectionlists_Consultants_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "Consultants",
                        principalColumn: "consultantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubcontractingWorks",
                columns: table => new
                {
                    SubcontractingWorkID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    unitPrice = table.Column<double>(type: "float", nullable: false),
                    priceWithVat = table.Column<double>(type: "float", nullable: false),
                    ContractID = table.Column<int>(type: "int", nullable: false),
                    attachment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubcontractingWorks", x => x.SubcontractingWorkID);
                    table.ForeignKey(
                        name: "FK_SubcontractingWorks_Contracts_ContractID",
                        column: x => x.ContractID,
                        principalTable: "Contracts",
                        principalColumn: "ConId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourcePlans",
                columns: table => new
                {
                    equipmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GranderFK = table.Column<int>(type: "int", nullable: false),
                    unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    budget = table.Column<double>(type: "float", nullable: false),
                    GranderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourcePlans", x => x.equipmentId);
                    table.ForeignKey(
                        name: "FK_ResourcePlans_Granders_GranderId",
                        column: x => x.GranderId,
                        principalTable: "Granders",
                        principalColumn: "GranderId");
                });

            migrationBuilder.CreateTable(
                name: "SubcontractingPlans",
                columns: table => new
                {
                    subcontractingPlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subcontractor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estimatedAmount = table.Column<double>(type: "float", nullable: false),
                    GranderFK = table.Column<int>(type: "int", nullable: false),
                    GranderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubcontractingPlans", x => x.subcontractingPlanId);
                    table.ForeignKey(
                        name: "FK_SubcontractingPlans_Granders_GranderId",
                        column: x => x.GranderId,
                        principalTable: "Granders",
                        principalColumn: "GranderId");
                });

            migrationBuilder.CreateTable(
                name: "WorkForcePlans",
                columns: table => new
                {
                    laborId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GranderFK = table.Column<int>(type: "int", nullable: false),
                    labor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    number = table.Column<int>(type: "int", nullable: false),
                    budget = table.Column<double>(type: "float", nullable: false),
                    GranderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkForcePlans", x => x.laborId);
                    table.ForeignKey(
                        name: "FK_WorkForcePlans_Granders_GranderId",
                        column: x => x.GranderId,
                        principalTable: "Granders",
                        principalColumn: "GranderId");
                });

            migrationBuilder.CreateTable(
                name: "Labors",
                columns: table => new
                {
                    laborId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeeklyRequirementFK = table.Column<int>(type: "int", nullable: false),
                    number = table.Column<int>(type: "int", nullable: false),
                    budget = table.Column<double>(type: "float", nullable: false),
                    WeeklyRequirementId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labors", x => x.laborId);
                    table.ForeignKey(
                        name: "FK_Labors_WeeklyRequirements_WeeklyRequirementId",
                        column: x => x.WeeklyRequirementId,
                        principalTable: "WeeklyRequirements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WeeklyEquipments",
                columns: table => new
                {
                    equipmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeeklyRequirementFK = table.Column<int>(type: "int", nullable: false),
                    unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    budget = table.Column<double>(type: "float", nullable: false),
                    WeeklyRequirementId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyEquipments", x => x.equipmentId);
                    table.ForeignKey(
                        name: "FK_WeeklyEquipments_WeeklyRequirements_WeeklyRequirementId",
                        column: x => x.WeeklyRequirementId,
                        principalTable: "WeeklyRequirements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WeeklyMaterials",
                columns: table => new
                {
                    materialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    materialItemId = table.Column<int>(type: "int", nullable: false),
                    WeeklyRequirementFK = table.Column<int>(type: "int", nullable: false),
                    unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    budget = table.Column<double>(type: "float", nullable: false),
                    WeeklyRequirementId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyMaterials", x => x.materialId);
                    table.ForeignKey(
                        name: "FK_WeeklyMaterials_Materials_materialItemId",
                        column: x => x.materialItemId,
                        principalTable: "Materials",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeeklyMaterials_WeeklyRequirements_WeeklyRequirementId",
                        column: x => x.WeeklyRequirementId,
                        principalTable: "WeeklyRequirements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedWorkLists_ConsultantId",
                table: "ApprovedWorkLists",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_DeclinedWorkLists_ConsultantId",
                table: "DeclinedWorkLists",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_DefectsCorrectionlists_ConsultantId",
                table: "DefectsCorrectionlists",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_EmployeeId",
                table: "Incidents",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Labors_WeeklyRequirementId",
                table: "Labors",
                column: "WeeklyRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourcePlans_GranderId",
                table: "ResourcePlans",
                column: "GranderId");

            migrationBuilder.CreateIndex(
                name: "IX_SubcontractingPlans_GranderId",
                table: "SubcontractingPlans",
                column: "GranderId");

            migrationBuilder.CreateIndex(
                name: "IX_SubcontractingWorks_ContractID",
                table: "SubcontractingWorks",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyEquipments_WeeklyRequirementId",
                table: "WeeklyEquipments",
                column: "WeeklyRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyMaterials_materialItemId",
                table: "WeeklyMaterials",
                column: "materialItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyMaterials_WeeklyRequirementId",
                table: "WeeklyMaterials",
                column: "WeeklyRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkForcePlans_GranderId",
                table: "WorkForcePlans",
                column: "GranderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkForces_AssignedWorkForceassigneWorkForceNo",
                table: "WorkForces",
                column: "AssignedWorkForceassigneWorkForceNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllocatedBudgets");

            migrationBuilder.DropTable(
                name: "AllocatedResources");

            migrationBuilder.DropTable(
                name: "ApprovedWorkLists");

            migrationBuilder.DropTable(
                name: "BIDs");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "DailyLabors");

            migrationBuilder.DropTable(
                name: "DeclinedWorkLists");

            migrationBuilder.DropTable(
                name: "DefectsCorrectionlists");

            migrationBuilder.DropTable(
                name: "Incidents");

            migrationBuilder.DropTable(
                name: "LaborDetails");

            migrationBuilder.DropTable(
                name: "Labors");

            migrationBuilder.DropTable(
                name: "ResourcePlans");

            migrationBuilder.DropTable(
                name: "SubcontractingPlans");

            migrationBuilder.DropTable(
                name: "SubcontractingWorks");

            migrationBuilder.DropTable(
                name: "SubContractors");

            migrationBuilder.DropTable(
                name: "SubContractWorks");

            migrationBuilder.DropTable(
                name: "TimeCards");

            migrationBuilder.DropTable(
                name: "WeeklyEquipments");

            migrationBuilder.DropTable(
                name: "WeeklyMaterials");

            migrationBuilder.DropTable(
                name: "WorkForcePlans");

            migrationBuilder.DropTable(
                name: "WorkForces");

            migrationBuilder.DropTable(
                name: "Consultants");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "WeeklyRequirements");

            migrationBuilder.DropTable(
                name: "Granders");

            migrationBuilder.DropTable(
                name: "AssignedWorkForces");
        }
    }
}
