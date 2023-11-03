Describe "PowerChart API" {
	BeforeAll {
		Import-Module .\bin\Debug\net6.0-windows\PowerChart.dll
	}
	It "Shows Chart" {
		Show-Chart
	}
}