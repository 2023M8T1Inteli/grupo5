import ButtonMin from "@/app/components/ButtonMin";
import Heading from "@/app/components/Heading";
import Subheading from "@/app/components/Subheading";
import Table from "@/app/components/Table";
import { TherapyItem } from "@/app/components/TherapyItem";

export interface ITherapy {
	name: string;
	date: string;
	createdBy: {
		name: string;
		username: string;
	};
	lastExecution: string;
	executionCount: number;
	lastPatient: {
		name: string;
		username: string;
	};
} 

export default function Therapies() {
    const therapies : ITherapy[] = [
        {
            name: 'Terapia 1',
            date: '10/10/2021',
            createdBy: {
                name: 'Ana Carolina',
                username: 'anacarolina'
            },
            lastExecution: '10/10/2021',
            executionCount: 10,
            lastPatient: {
                name: 'Maria Luiza',
                username: 'marialuiza'
            }
        },
    ];

    const headers = [
		{name: 'Nome', spacing: '64'}, 
		{name: 'Data de criação', spacing: '44'},
		{name: 'Criado por', spacing: '44'},
		{name: 'Última execução', spacing: '44'},
		{name: 'Nº de execuções', spacing: '40'},
		{name: 'Último paciente a executar', spacing: '52'},
		{name: '', spacing: '40'}
	];

    return (
        <div className='flex flex-col p-16 w-[85%] gap-16'>
            <div className='flex justify-between items-center'>
                <div className='flex flex-col gap-2'>
                    <Heading>Terapias</Heading>
                    <Subheading>Gerencie as terapias disponíveis</Subheading>
                </div>
                <div className='w-48'>
                    <ButtonMin text='Criar terapia'/>	
                </div>
            </div>

            <Table headers={headers}>
                {therapies.map((therapy, index) => (
                    <TherapyItem key={index} therapy={therapy} />
                ))}
            </Table>
        </div>
    );
}
