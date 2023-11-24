'use client'
import { Doughnut } from 'react-chartjs-2';
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';

ChartJS.register(ArcElement, Tooltip, Legend);

export interface DonutChartPropsData {
    labels: string[];
    datasets: {
      data: number[];
      backgroundColor: string[];
      borderColor?: string[];
      borderWidth?: number;
    }[];
  }

interface DonutChartProps {
  data: DonutChartPropsData
}

const DonutChart: React.FC<DonutChartProps> = ({ data }) => {
  return <Doughnut data={data} options={{cutout: '70%', plugins: {legend: {position: 'left'}}}}/>;
};

export default DonutChart;
