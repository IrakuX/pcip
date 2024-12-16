<template>
    <Card class="max-w-sm mx-auto">
        <CardHeader>
            <CardTitle class="text-2xl">
                Login
            </CardTitle>
            <CardDescription>
                Enter your email below to login to your account
            </CardDescription>
        </CardHeader>
        <CardContent>
            <form @submit.prevent="fnSubmitForm">
                <div class="grid gap-4">
                    <div class="grid gap-2">
                        <Label for="email">Email</Label>
                        <Input v-model="form.email" type="email" placeholder="m@example.com" required />
                    </div>
                    <div class="grid gap-2">
                        <div class="flex items-center">
                            <Label for="password">Password</Label>
                            <NuxtLink to="/auth/forgot" class="inline-block ml-auto text-sm underline">
                                Forgot your password?
                            </NuxtLink>
                        </div>
                        <Input v-model="form.password" type="password" required />
                    </div>
                    <Button type="submit" class="w-full">
                        Login
                    </Button>
                </div>
                <div class="hidden mt-4 text-sm text-center">
                    Don't have an account?
                    <NuxtLink to="/auth/register" class="underline">
                        Sign up
                    </NuxtLink>
                </div>
            </form>
        </CardContent>
    </Card>
</template>

<script setup lang="ts">
import type { ValidationErrors } from '~/types/ValidationErrors';

const form = ref({
    email: "",
    password: "",
});

const busy = ref(false);
const errors = ref<ValidationErrors>({});

const fnSubmitForm = () => {
    try {
        errors.value = {};
        busy.value = true;

        console.log(form.value);

        // // Callback function example to add 2FA feature.
        // await login<{ two_factor: boolean }>(form.value, {}, async (response) => {
        //   if (response.two_factor) {
        //     return await navigateTo(`/2fa`);
        //   }
        //   return await navigateTo(`/dashboard`);
        // });
    } catch (err) {
        busy.value = false;
        if (err instanceof FetchError && err.response?.status === 422) {
            errors.value = err.response._data.errors;
        }
    }
};

</script>

<style scoped></style>
