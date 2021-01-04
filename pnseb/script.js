google.charts.load('current', {'packages':['corechart']});
google.charts.setOnLoadCallback(drawChart);
var intervalsetted=false;
function drawChart() {
	
	$.get("dataprovider.php",(values)=>{
		values=JSON.parse(values);
		values.pop();
		var dataArr=[];
		dataArr.push(["T","Metan","Karbonmonoksit","Hidrojen"]);
		for(var i=0;i<values.length;i++){
		 dataArr.push([i,parseFloat(values[i][0].substring(2)),parseFloat(values[i][1].substring(2)),parseFloat(values[i][2].substring(2))]);
		}
	
		var data = google.visualization.arrayToDataTable(dataArr);

		var options = {
		  title: 'Sensör verileri',
		  curveType: 'function',
		  legend: { position: 'bottom' }
		};

		var chart = new google.visualization.LineChart(document.getElementById('curve_chart'));

		chart.draw(data, options);
		if(!intervalsetted){setInterval(drawChart,1000);intervalsetted=true;}
});
}