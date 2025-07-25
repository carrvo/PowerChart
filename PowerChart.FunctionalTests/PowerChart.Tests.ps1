switch ($PSVersionTable.PSVersion.Major) {
	5 {
		Import-Module $PSScriptRoot\..\bin\Debug\PowerChart.psd1
		Update-TypeData -AppendPath $PSScriptRoot\..\bin\Debug\PowerChart.Types.ps1xml
	}
	7 {Import-Module $PSScriptRoot\..\bin\Debug\PowerChart.psd1}
	default {Import-Module $PSScriptRoot\..\bin\Debug\PowerChart.psd1}
}

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
		$chart.BackColor = 'Cyan'
		$chart.AxisColor = 'LimeGreen'
		Add-Scatter -Chart $chart -XCoordinate 10 -YCoordinate 50 -Color Red
		Add-Scatter -Chart $chart -XCoordinate 20 -YCoordinate 90 -Color Red -Size 5
		Add-Scatter -Chart $chart -XCoordinate 30 -YCoordinate 50 -Color Red -Size 10.5
		$chart.YMin = 0
		$chart.YMax = 100
		Show-Chart -Chart $chart
		$chart.Dialog.Join()
	}
	It "Draws a Scatter Chart" {
		$chart = New-Chart
		$chart.Title = 'Get-Process (scatter)'
		$chart.XAxisLabel = 'Process ID'
		$chart.YAxisLabel = 'CPU'
		Get-Process | Add-Scatter -Chart $chart -XProperty Id -YProperty CPU -Color Red -ErrorAction SilentlyContinue
		Show-Chart -Chart $chart
		$chart.Dialog.Join()
	}
	It "Draws multiple Scatter" {
		$chart = New-Chart
		$chart.Title = 'Get-Process (multiple)'
		$chart.XAxisLabel = 'Process ID'
		$chart.YAxisLabel = 'CPU/ID'
		$processes = Get-Process
		$processes | Add-Scatter -Chart $chart -XProperty Id -YProperty CPU -ErrorAction SilentlyContinue
		$processes | Add-Scatter -Chart $chart -XProperty Id -YProperty Id -ErrorAction SilentlyContinue
		Show-Chart -Chart $chart
		$chart.Dialog.Join()
	}
	It "Draws a Line Chart" {
		$chart = New-Chart
		$chart.Title = 'Get-Process (line)'
		$chart.XAxisLabel = 'Process ID'
		$chart.YAxisLabel = 'CPU'
		Get-Process | Add-Line -Chart $chart -XProperty Id -YProperty CPU -Color Red -ErrorAction SilentlyContinue
		Show-Chart -Chart $chart
		$chart.Dialog.Join()
	}
	It "Draws a Bar Chart" {
		$chart = New-Chart
		$chart.Title = 'Get-Process (bar)'
		$chart.XAxisLabel = 'Process ID'
		$chart.YAxisLabel = 'CPU'
		Get-Process -PipelineVariable process |
			Where-Object CPU -LT 30 |
			ForEach-Object { # ensures CPU is minimum of 1
				switch ($process) {
					{$_.CPU -LT 1} {
						$p = [PSCustomObject]@{
							Id = $_.Id
							CPU = 1
						}
						Write-Output $p
					}
					default {
						Write-Output $_
					}
				}
			} |
			Add-Bar -Chart $chart -XProperty Id -YProperty CPU -Color Red -ErrorAction SilentlyContinue
		$chart.YMin = 0
		Show-Chart -Chart $chart
		$chart.Dialog.Join()
	}
}