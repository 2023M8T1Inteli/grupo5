import Heading from '@/app/components/Heading';
import Subheading from '../components/Subheading';
import NextPatient from '../components/NextPatient';
import ButtonCard from '../components/ButtonCard';
import Heart from '@/public/whiteheart.svg'
import Profile from '@/public/profile_white.svg'

const patientsData = [
  { name: 'Ana Júlia', hour: '13:30', data: 'Hoje, 16 de março de 2023' },
  { name: 'Pedro Henrique', hour: '15:00', data: 'Hoje, 16 de março de 2023' },
  { name: 'Maria Luiza', hour: '16:30', data: 'Hoje, 16 de março de 2023' },
];

const shortcutsData = [
  { icon: Profile, text: 'Adicionar novo paciente' },
  { icon: Heart, text: 'Criar nova terapia' },
];

const PatientsList = () => (
  <div className='flex flex-col gap-4'>
    {patientsData.map(patient => (
      <NextPatient key={patient.name} {...patient} />
    ))}
  </div>
);

const ShortcutsList = () => (
  <div className='flex gap-8'>
    {shortcutsData.map((shortcut, index) => (
      <ButtonCard key={index} {...shortcut} />
    ))}
  </div>
);

export default function Home() {
  return (
    <div className='p-16 flex flex-col gap-16 w-[85%]'>
      <div className='grid gap-2'>
        <Heading>Início</Heading>
        <Subheading>Confira a agenda do dia, cadastre novos pacientes e crie novas terapias</Subheading>
      </div>
      <div className='flex gap-16'>
        <div className='flex flex-col gap-8'>
          <h2 className='text-4xl'>Próximos pacientes</h2>
          <PatientsList />
        </div>
        <div className='flex flex-col items-start gap-8'>
          <h2 className='text-4xl'>Atalhos</h2>
          <ShortcutsList />
        </div>
      </div>
    </div>
  )
}
