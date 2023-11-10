Import-Module $PSScriptRoot\..\bin\Debug\net6.0-windows\PowerChart.dll

Describe "PowerChart API" {
	It "Shows Chart" {
		$chart = New-Chart
		Show-Chart -Chart $chart
		$chart.Dialog.Join()
	}
	It "Shows Points" {
		$chart = New-Chart
		$chart.Title = 'Shows Points'
		$chart.XAxisLabel = 'time'
		$chart.YAxisLabel = 'Energy'
		Add-Scatter -Chart $chart -XCoordinate 10 -YCoordinate 50
		Add-Scatter -Chart $chart -XCoordinate 20 -YCoordinate 100
		Add-Scatter -Chart $chart -XCoordinate 30 -YCoordinate 50
		Show-Chart -Chart $chart
		$chart.Dialog.Join()
	}
	It "Draws a Scatter Chart" {
		$chart = New-Chart
		$chart.Title = 'Get-Process'
		$chart.XAxisLabel = 'Process ID'
		$chart.YAxisLabel = 'CPU'
		Get-Process | Add-Scatter $chart -XProperty Id -YProperty CPU -ErrorAction SilentlyContinue
		Show-Chart -Chart $chart
		$chart.Dialog.Join()
	}
}