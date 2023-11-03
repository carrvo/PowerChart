Describe "PowerChart API" {
	BeforeAll {
		Import-Module .\bin\Debug\net6.0-windows\PowerChart.dll
	}
	It "Shows Chart" {
		$chart = New-Chart
		Show-Chart -Chart $chart
	}
}