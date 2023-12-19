import DonutChart, { DonutChartPropsData } from "@/app/components/DonutChart";
import Heading from "@/app/components/Heading";
import SectionData from "@/app/components/SectionData";
import SectionTitle from "@/app/components/SectionTitle";
import Subheading from "@/app/components/Subheading";

export default function Metrics() {

	const data : DonutChartPropsData = {
		labels: ['Erros', 'Acertos'],
		datasets: [
			{
				data: [329, 940],
				backgroundColor: ['#E7343F', '#12B76A'],
				borderColor: ['#E7343F', '#12B76A'],
				borderWidth: 0,
			},
		],
	}

	return(
		<div className='w-[85%]'>
			<div className='flex flex-col p-16  gap-16'>
				<div className='flex justify-between items-center'>
					<div className='flex flex-col gap-2'>
						<Heading>Estatísticas</Heading>
						<Subheading>Acompanhe métricas sobre os pacientes aqui</Subheading>
					</div>
				</div>
				<div className='flex flex-col gap-16'>
					<div className='flex flex-col gap-4'>
						<SectionTitle>Tempo médio por terapia</SectionTitle>
						<SectionData>50 minutos e 20 segundos</SectionData>
					</div>
					<div className='flex flex-col gap-4'>
						<SectionTitle>Erros e acertos</SectionTitle>
						<div className="w-72">
							<DonutChart data={data}/>
						</div>
					</div>
				</div>
        	</div>
		</div>
	)
}