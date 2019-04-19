<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Final_Project.KickstarterPredictor" %>

<!DOCTYPE html>
<script runat="server">

    
</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Kickstarter Predictor</title> 
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css"/>
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <link rel="stylesheet" type="text/css" href="Content/Site.css"/>
    <script type="text/javascript" src="https://public.tableau.com/javascripts/api/tableau-2.0.0.min.js"></script>

    <script>
        function initViz() {
            var containerDiv = document.getElementById("vizContainer"),
                url = "https://public.tableau.com/views/415Final_15556217332110/Analytics?:embed=y&:display_count=yes&publish=yes";

            var viz = new tableau.Viz(containerDiv, url);
        }

        function showResults() {
            var results = document.getElementById("resultsBox");

            results.setAttribute("display", "block");
        }
    </script>

</head>
<body onload="initViz()">
    <div>
        <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/b/b5/Kickstarter_logo.svg/1280px-Kickstarter_logo.svg.png" alt="kickstarterLogo" class="logo"/>

        <p class="siteHeader">Campaign Success Prediction Calculator</p>
    </div>
    

    <div class="buttonRow">
        <a class="btn rowButton leftButton" href="#aboutUs">About the Project</a>
        <a class="btn rowButton rightButton" href="#analytics">Analytics</a>
    </div>

    <div class="formDiv">
        <form id="form1" runat="server" class="formBox">

            
            <div class="formLeft">

                <div class="form-group">
                    <asp:Label ID="lblSubCategory" runat="server" Text="Category:" CssClass="formLabel"></asp:Label>
                    <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="formResponse"></asp:DropDownList>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblCountry" runat="server" Text="Country:" CssClass="formLabel"></asp:Label>
                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="formResponse"></asp:DropDownList>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblGoal" runat="server" Text="Goal (USD):" CssClass="formLabel"></asp:Label>
                    <asp:TextBox ID="txtGoal" runat="server" CssClass="formResponse"></asp:TextBox>
                </div>

            </div>

            <div class=" formRight">

                <div class="form-group">
                    <asp:Label ID="lblLaunchDay" runat="server" Text="Launch Day:" CssClass="formLabel"></asp:Label>
                    <asp:DropDownList ID="ddlDay" runat="server" CssClass="formResponse"></asp:DropDownList>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblLaunchMonth" runat="server" Text="Launch Month:" CssClass="formLabel"></asp:Label>
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="formResponse"></asp:DropDownList>
                </div>

                 <div class="form-group">
                    <asp:Label ID="lblDuration" runat="server" Text="Duration (Days):" CssClass="formLabel"></asp:Label>
                    <asp:TextBox ID="txtDuration" runat="server" CssClass="formResponse"></asp:TextBox>
                </div>

            </div>

            <asp:Button ID="btnPredict" runat="server" Text="Predict Success" OnClick="makePredictions" CssClass="btn submitButton"/>

        </form>

    </div>
    <div class="formDiv">

        <div class="formBox">
                        
            <div class="results">
                <h1>Results</h1>
                <br />
                <asp:Label ID="lblBackers" runat="server" Text="" style="font-size: 20px;"></asp:Label>
                <br />
                <asp:Label ID="lblPercent" runat="server" Text="" style="font-size: 20px;"></asp:Label>
                <br />
                <asp:Label ID="lblStatus" runat="server" Text="" style="font-size: 20px;"></asp:Label>
            </div>
        </div>

    </div>


    <div class="analytics" id="analytics">
        <div id="vizContainer"></div>
    </div>

    <div class="aboutUs" id="aboutUs">
        <p class="aboutTitle">About the Project</p>
        <div class="aboutDiv">
            <div class="row aboutCard">
                <div class="col-3">
                    <div class="nav flex-column nav-pills" id="v-pills-tab" role="tablist" aria-orientation="vertical">
                        <a class="nav-link active" id="generalTab" data-toggle="pill" href="#generalText" role="tab" aria-controls="v-pills-home" aria-selected="true">General</a>
                        <a class="nav-link" id="modelsTab" data-toggle="pill" href="#modelsText" role="tab" aria-controls="v-pills-profile" aria-selected="false">Models & Algorithms</a>
                        <a class="nav-link" id="visualizationTab" data-toggle="pill" href="#visualizationText" role="tab" aria-controls="v-pills-messages" aria-selected="false">Data Visualization</a>
                    </div>
                </div>
                <div class="col-8">
                    <div class="tab-content" id="v-pills-tabContent">
                        <div class="tab-pane fade show active" id="generalText" role="tabpanel" aria-labelledby="v-pills-home-tab">We are a group of Information Systems undergrad students at Brigham Young University in Provo, UT. 
                                We created this prediction calculator as part of an assignment for our Data Analytics and Machine Learning coourse.
                                We utilized Azure Machine Learning Studio for the algorithms and Tableau for the data visualizations.
                                The dataset came from Kaggle.com</div>

                        <div class="tab-pane fade" id="modelsText" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                            In order to generate predictions based on user input we created a number of regression and classification models in Microsoft Azure Machine Learning Studio and chose the most accurate one.
                            We settled on three different models: one to predict the ultimate success or failure of a project, one to predict the number of backers, and one to predict the percent of its goal a project would reach. 
                            The number of backers and the percent of goal reached both use regression models, and a classification model is used to predict the overall success of the project. 
                            The models allow for new data to be fed into them to aid in their training and improve their overall accuracy.

                        </div>

                        <div class="tab-pane fade" id="visualizationText" role="tabpanel" aria-labelledby="v-pills-messages-tab">
                            Using Tableau we were able to create a story consisting of data vasualizations depicting different trends in the data. 
                            Our goal in doing this was to determine which variables had a significant effect on the success of a Kickstarter project, including how many backers a project received, and what percentage of the goal was funded. 
                            We used a variety of methods to do this and included our favorite visualizations in the story, as well an interactive dashboard. 
                            Feel free to explore the Tableau story using the provided filters and functions to better understand the data.
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <p class="aboutTitle">The Team</p>
        <div class="card-deck">
            <div class="card teamCard">
                <img src="Content/Images/Ryan.jpg" class="card-img-top" alt="Ryan"/>
                <div class="card-body">
                    <h5 class="card-title">Ryan Skalla</h5>
                    <p class="card-text"></p>
                </div>
            </div>

            <div class="card teamCard">
                <img src="Content/Images/Matt.png" class="card-img-top" alt="Ryan"/>
                <div class="card-body">
                    <h5 class="card-title">Matthew Clement</h5>
                    <p class="card-text">
                    </p>
                </div>
            </div>

            <div class="card teamCard">
                <img src="Content/Images/Steven.jpg" class="card-img-top" alt="Ryan"/>
                <div class="card-body">
                    <h5 class="card-title">Steven Rummler</h5>
                    <p class="card-text"></p>
                </div>
            </div>

            <div class="card teamCard">
                <img src="Content/Images/Jaxon.jpg" class="card-img-top" alt="Ryan"/>
                <div class="card-body">
                    <h5 class="card-title">Jaxon Moffitt</h5>
                    <p class="card-text"></p>
                    
                </div>
            </div>
        </div> 


    </div>
    <footer class="footer">

        <p class="copyright">SCRM Consulting, Copyright <%=DateTime.Now.Year%> &copy;</p>

    </footer>

</body>
</html>