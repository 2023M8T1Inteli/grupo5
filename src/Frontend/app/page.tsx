import Image from 'next/image'
import Logo from '../public/logo.svg'
import Heading from './components/Heading'
import Subheading from './components/Subheading'
import LoginForm from './components/LoginForm'

const Login = () => {
  return (
    <div className='flex items-start justify-between w-[100vw] h-[100vh] p-24 bg-[url("/red_boy_1.svg")] bg-no-repeat bg-[left_25%_top] bg-scal bg-[length:auto_90%]'>
      <Image src={Logo} alt='Logotipo'/>
      <div className='flex items-center h-full'>
        <div className='flex flex-col gap-8'>
          <div className='mb-8'>
            <Heading className='mb-2'>Portal do terapeuta</Heading>
            <Subheading>Preencha com suas credenciais para continuar</Subheading>
          </div>
          <LoginForm />
        </div>
      </div>
    </div>
  )
}

export default Login
