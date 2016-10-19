(function (speak) {
    require.config({
        paths: { ga: "/sitecore/shell/client/Applications/GoogleAnalytics/Scripts/ga" }
    }); 

    speak.component(["ga"], {
        name: "ChartByGoogle",
        initialized: function () {           
            this.LoadGACharts(this);
        },
        LoadGACharts: function (app) {
            gapi.analytics.ready(function () {
                /* STEP 1: Authorize against Google via Service Account */
                gapi.analytics.auth.authorize({
                    serverAuth: { access_token: app.GAAuthorizationToken }
                });

                /* STEP 2: Create the chart query based on user defined values */
                var dataChart = new
                    gapi.analytics.googleCharts.DataChart({
                        query: {
                            metrics: app.Metrics,
                            dimensions: app.Dimensions,
                            'start-date': app.ChartByGoogleStartDate,
                            'end-date': app.ChartByGoogleEndDate,
                            'max-results': app.MaxResult//,
                            //sort:app.Sort
                        },
                        chart: {
                            container: app.id,
                            type: app.ChartType,
                            options: {
                                width: '100%'
                            }
                        }
                    });

                /* STEP 3: Execute the query against the profile */
                dataChart.set({ query: { ids: app.ProfileId } }).execute();                

            });//END ga ready
        }//END loadgacharts fnc
    });//END speak declaration
})(Sitecore.Speak);





